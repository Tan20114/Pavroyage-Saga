using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterAI
{
    readonly Monster owner;

    #region Action Related
    readonly SkillPriorityFinder priorityFinder;
    readonly SkillSelector skillSelector;
    #endregion

    #region Target Selection
    readonly CandidateGetter candidateGetter;
    ITargetSelectionStrategy targetSelectionStrategy;
    #endregion

    #region Behavior Tree
    readonly BehaviorNode behaviorTreeRoot;
    readonly Blackboard blackboard;
    #endregion

    AIBattleContext context;

    public MonsterAI(Monster owner)
    {
        this.owner = owner;

        priorityFinder = new SkillPriorityFinder();

        skillSelector = new SkillSelector(priorityFinder);

        candidateGetter = new CandidateGetter();
        targetSelectionStrategy = new SmartTargetStrategy(
            new List<ITargetCalculator>
            {
                new LowHPWeightCalculation(),
                new MaxHPWeightCalculation(),
                new AggroWeightCalculation(),
            }
        );

        blackboard = new Blackboard()
        {
            Self = owner
        };

        behaviorTreeRoot = CreateBehaviorTree();
    }

    public void InitializeTurn(IReadOnlyList<Character> enemies, IReadOnlyList<Character> allies)
    {
        context = new AIBattleContext(owner, allies, enemies);
    }

    #region Node Actions
    bool IsLowHP()
    {
        return owner.CurrentHP <= owner.derivedStats.GetDerived(DerivedType.MaxHp) * 0.3f;
    }

    bool CanHeal()
    {
        return owner.skills.Any(skill =>
            skill.skillType == SkillType.Heal &&
            skill.skillPointCost <= owner.SkillPoints
        );
    }

    bool CanAct()
    {
        return context.Enemies.Any(enemy => !enemy.IsDead);
    }

    bool CanUseSkill()
    {
        SkillPriorityResult result = skillSelector.Select(
            context,
            owner.skills
        );

        if (result == null) return false;

        blackboard.SelectedSkill = result.Skill;

        return true;
    }

    NodeStatus UseSkill()
    {
        SkillData skill = blackboard.SelectedSkill;

        if (skill == null) return NodeStatus.Failure;

        IReadOnlyList<Character> candidates = candidateGetter.GetCandidates(
                context,
                skill
            );

        Character target = targetSelectionStrategy.SelectTarget(
                context.Self,
                candidates
            );

        if (target == null) return NodeStatus.Failure;

        blackboard.SelectedAction = AIActionType.Skill;
        blackboard.Target = target;

        Debug.Log($"[AI] {owner.name} chose Skill: {skill.skillName}");

        return NodeStatus.Success;
    }

    NodeStatus Attack()
    {
        Character target =
            targetSelectionStrategy.SelectTarget(
                context.Self,
                context.Enemies
            );

        if (target == null)
            return NodeStatus.Failure;

        blackboard.SelectedAction = AIActionType.Attack;
        blackboard.Target = target;

        Debug.Log(
            $"[AI] {owner.name} chose Attack"
        );

        return NodeStatus.Success;
    }
    #endregion

    #region Action
    public IBattleAction DecideAction()
    {
        blackboard.Target = null;
        blackboard.SelectedSkill = null;
        blackboard.SelectedAction = default;

        NodeStatus status = behaviorTreeRoot.Tick();

        if (status != NodeStatus.Success)
            return null;

        return BuildAction();
    }

    IBattleAction BuildAction()
    {
        switch (blackboard.SelectedAction)
        {
            case AIActionType.Attack:
                if (blackboard.Target == null) return null;

                return new AttackAction(
                    blackboard.Self,
                    blackboard.Target
                );
            case AIActionType.Skill:
                if (blackboard.Target == null || blackboard.SelectedSkill == null) return null;

                return new SkillAction(
                    blackboard.Self,
                    blackboard.Target,
                    blackboard.SelectedSkill
                );
            default:
                return null;
        }
    }
    #endregion

    #region Behavior Tree
    BehaviorNode CreateBehaviorTree()
    {
        BehaviorSelectorNode root =
            new BehaviorSelectorNode(blackboard);

        #region Emergency Heal Path
        BehaviorSequenceNode emergencyHeal =
            new BehaviorSequenceNode(blackboard);

        emergencyHeal.AddChild(
            new BehaviorConditionNode(
                IsLowHP,
                blackboard
            )
        );

        emergencyHeal.AddChild(
            new BehaviorConditionNode(
                CanHeal,
                blackboard
            )
        );

        emergencyHeal.AddChild(
            new BehaviorActionNode(
                EmergencyHeal,
                blackboard
            )
        );
        #endregion

        #region Normal Action Path
        BehaviorSequenceNode normal = new BehaviorSequenceNode(blackboard);

        normal.AddChild(new BehaviorConditionNode(
                CanAct,
                blackboard
            )
        );

        BehaviorSelectorNode normalSelector = new BehaviorSelectorNode(blackboard);

        #region Skill
        BehaviorSequenceNode trySkill = new BehaviorSequenceNode(blackboard);

        trySkill.AddChild(
            new BehaviorConditionNode(
                CanUseSkill,
                blackboard
            )
        );

        trySkill.AddChild(
            new BehaviorActionNode(
                UseSkill,
                blackboard
            )
        );
        #endregion

        #region Attack
        normalSelector.AddChild(trySkill);

        normalSelector.AddChild(
            new BehaviorActionNode(
                Attack,
                blackboard
            )
        );

        normal.AddChild(normalSelector);
        #endregion
        #endregion

        root.AddChild(emergencyHeal);
        root.AddChild(normal);

        return root;
    }

    NodeStatus EmergencyHeal()
    {
        SkillPriorityResult result = skillSelector.Select(
                context,
                owner.skills,
                SkillType.Heal
        );

        if (result == null) return NodeStatus.Failure;

        blackboard.SelectedAction = AIActionType.Skill;
        blackboard.SelectedSkill = result.Skill;
        blackboard.Target = owner;

        Debug.Log($"[AI] {owner.name} chose Emergency Heal: {result.Skill.skillName}");

        return NodeStatus.Success;
    }
    #endregion
}