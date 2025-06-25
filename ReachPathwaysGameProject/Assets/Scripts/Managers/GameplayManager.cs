using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using MemoryCards;
using AbilityCards;
using SupportCards;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField]
    public GameObject cardPrefab, characterParent, actionsMenu, hand, skillDeck, scenarioDeck;
    [SerializeField]
    public RoundUI roundUI;

    [SerializeField]
    private int totalRounds = 4;

    // Note that currentRound will start at 1 instead of 0 for text display purposes
    public int currentRound = 1;

    public int currentTurn { get; private set; } = 0;

    public bool endAllRounds { get; private set; }

    [SerializeField]
    public List<CharacterCard> characterList = new();

    private List<GameObject> handList = new();

    private List<CardBase> playerDeck = new();
    
    // Innovator Cards
    public static List<Type> innovatorCards = new()
    {
        typeof(EurekaCard),
        typeof(UnconventionalHackCard),
        typeof(AdaptTheWorldCard),
        typeof(ArtTherapyCard),
        typeof(WeatherTheBrainstormCard),
        typeof(BlueElectricityWhiteSmokeCard),
        typeof(MissingEyebrowsCard),
        typeof(StuckInARutCard),
        typeof(WatchItBurnCard)
    };

    // Strategist Cards
    public static List<Type> strategistCards = new()
    {
            typeof(TimedJustRightCard),
            typeof(AllInOnePieceCard),
            typeof(BreatheInBreatheOutCard),
            typeof(PerceivedRisksCard),
            typeof(RestAndRecuperateCard),
            typeof(FriendInNeedCard),
            typeof(EmotionalOutburstCard),
            typeof(TearToPiecesCard),
            typeof(LackOfAwarenessCard)
    };

    // Visionary Cards
    public static List<Type> visionaryCards = new()
    {
        typeof(APinchOfPunctualityCard),
        typeof(TheGiftOfAVisionCard),
        typeof(PromisesPromisesCard),
        typeof(ElephantInTheRoomCard),
        typeof(AdvocationPracticesCard),
        typeof(DelayedAccountabilityCard),
        typeof(TheBiggerPictureCard),
        typeof(SuperSonicPotentialCard),
        typeof(OverestimatedAbilitiesCard),
        typeof(TheCurseOfAVisionCard),
        typeof(AstronomicalRecalculationCard)
    };

    // Collaborator Cards
    public static List<Type> collaboratorCards = new()
    {
        typeof(AHelpingHand),
        typeof(WeListenAndWeDontJudgeCard),
        typeof(TakingInitiativeCard),
        typeof(DungeonsAndDelegationsCard),
        typeof(TrustFallCard),
        typeof(MoodBoardCard),
        typeof(PartyRockerCard),
        typeof(OpenNoteQuizCard),
        typeof(BurntAndCrunchedCard),
        typeof(FightForTheCrownCard),
        typeof(TooManyEggsForOneBasketCard)
    };

    // Communicator Cards
    public static List<Type> communicatorCards = new()
    {
        typeof(AllsWellThatEndsWellCard),
        typeof(FruitfulTruthsCard),
        typeof(TalkOfTheTownCard),
        typeof(StalematesCard),
        typeof(AdjustingConnectionsCard),
        typeof(AForEffortCard),
        typeof(ClearSummationCard),
        typeof(HighStakesPitchCard),
        typeof(AllEarsNoMouthCard),
        typeof(InterruptionsCard),
        typeof(DetrimentalMisstepCard)
    };

    // Ability Cards
    public static List<Type> abilityCards = new()
    {
        // Awareness Ability Cards
        typeof(GroundedFocusCard),
        typeof(RefocusCard),
        typeof(MentalClarityCard),
        typeof(TacticalResetCard),
        // Creativity Ability Cards
        typeof(CreativeSparkCard),
        typeof(TriageCard),
        typeof(ImagineAWorldCard),
        typeof(BufferCard),
        // Integrity Ability Cards
        typeof(MomentumCard),
        typeof(PauseAndReflectCard),
        typeof(PreparationPaysCard),
        typeof(SteadyNervesCard),
        // Teamwork Ability Cards
        typeof(ReorganizeResourcesCard),
        typeof(BoostMoraleCard),
        typeof(QuickAssessmentCard),
        typeof(GroupCheckInCard),
        // Communication Ability Cards
        typeof(SynchronizeCard),
        typeof(SwapSupportCard),
        typeof(ExtendAHandCard),
        typeof(CollectiveWisdomCard)
    };

    // Support Cards
    public static List<Type> supportCards = new()
    {
        // Awareness Support Cards
        typeof(DeepBreathCard),
        typeof(ChallengingAssumptionCard),
        typeof(ClearObservationCard),
        typeof(EmpatheticEyesCard),
        typeof(WorldlyImpactCard),
        typeof(SocialWavesCard),
        // Creativity Support Cards
        typeof(SparklingRealizationCard),
        typeof(InspirationOfLightCard),
        typeof(ExpressiveConnectionCard),
        typeof(FuturePossibilitiesCard),
        typeof(ResourcefulPurposeCard),
        typeof(ReadyForTheUnknownCard),
        // Integrity Support Cards
        typeof(ValuableActionCard),
        typeof(HardTruthCard),
        typeof(MistakesWillPassCard),
        typeof(ProbableThinkingCard),
        typeof(WhoCallsTheNormCard),
        typeof(HonorFocusCard),
        // Teamwork Support Cards
        typeof(SharedTrustCard),
        typeof(AdaptNRelyCard),
        typeof(CelebrationUnitesCard),
        typeof(ValuedInputCard),
        typeof(HarmonizeLightCard),
        typeof(ShoulderOfSupportCard),
        // Communication Support Cards
        typeof(PurposeOfWordsCard),
        typeof(ActiveListeningCard),
        typeof(IntentionalClarityCard),
        typeof(VulnerableRootsCard),
        typeof(TalesFlutterCard),
        typeof(AmplifyEndsCard)
    };

    void Awake()
    {
        // Singleton enforcement
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        scenarioDeck.SetActive(false);
        
        // Check if we're in GameInitState and transition to ScenarioDrawState
        // This ensures GameplayManager is fully initialized before ScenarioDrawState is entered
        if (StateManager.Instance != null && 
            StateManager.Instance.GetCurrentState() is GameInitState)
        {
            StateManager.Instance.ChangeState(new ScenarioDrawState());
        }
    }

    void Start()
    {
        characterList = characterParent.GetComponentsInChildren<CharacterCard>().ToList();
    }

    public void DrawSkillCard()
    {
        GameObject cardObj;
        handList.Add(cardObj = Instantiate(cardPrefab, hand.transform));
        cardObj.GetComponent<CardObj>().Init(innovatorCards[0]);

        if (StateManager.Instance.GetCurrentState() is SkillDrawState)
        {
            if (hand.GetComponentsInChildren<Transform>().Length - 1 > 2)
            {
                StateManager.Instance.ChangeState(new TurnState());
            }
        }
    }

    public void DrawScenarioCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        scenarioDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT SCENARIO";
        StateManager.Instance.ChangeState(new SkillDrawState());
    }

    public void AdvanceTurn()
    {
        if (currentTurn < characterList.Count - 1)
        {
            currentTurn++;
            roundUI.UpdateTurnText(characterList[currentTurn]);
        }
        else
        {
            // currentRound starts at 1, not 0, for text display purposes
            if (currentRound < totalRounds)
            {
                currentTurn = 0;
                currentRound++;
                roundUI.UpdateRoundText(currentRound);
            }
            else
            {
                roundUI.EndRounds();
            }
        }
    }
}
