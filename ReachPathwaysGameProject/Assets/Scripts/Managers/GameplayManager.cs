using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using MemoryCards;
using AbilityCards;
using SupportCards;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }
    private System.Random rng = new();

    [SerializeField]
    public GameObject cardPrefab, characterParent, actionsMenu, playerHandObj, playerDeckObj, scenarioDeck, abilityDeck;
    [SerializeField]
    public RoundUI roundUI;
    [SerializeField]
    public PointsUI pointsUI;

    [SerializeField]
    private int totalRounds = 4;

    // Note that currentRound will start at 1 instead of 0 for text display purposes
    public int currentRound = 1;

    private int cardsPlayedThisRound = 0;

    public int pointSum { get; private set; } = 0;

    public int currentTurn { get; private set; } = 0;

    public bool endAllRounds { get; private set; }

    [SerializeField]
    public List<CharacterCard> characterList = new();

    [SerializeField]
    private List<GameObject> playerHand = new();

    public List<Type> playerDeck = new();
    
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
        playerDeck.Add(innovatorCards[0]);
        playerDeck.Add(innovatorCards[1]);
    }

    public void DrawPlayerCard()
    {
        if (!(playerDeck.Count > 0))
        {
            Debug.Log("Player is out of cards!");
        }
        else
        {
            // Create the new card object & save it in the player's hand
            GameObject cardObj;
            playerHand.Add(cardObj = Instantiate(cardPrefab, playerHandObj.transform));
            // Draw a card & remove it from the player's deck
            int cardIndex = rng.Next(playerDeck.Count);
            cardObj.GetComponent<CardObj>().Init(playerDeck[cardIndex]);
            playerDeck.RemoveAt(cardIndex);
        }
        
        // Advances to the starting turn once the player has drawn 4 cards
        if (StateManager.Instance.GetCurrentState() is InitialDrawState)
        {
            // Enables drawing from the ability deck if the player ran out of cards in their deck on the initial draw
            if (playerDeck.Count < 1)
            {
                playerDeckObj.GetComponent<Button>().interactable = false;
                abilityDeck.GetComponent<Button>().interactable = true;
            }

            if (playerHandObj.GetComponentsInChildren<CardObj>().Length > 3)
            {
                StateManager.Instance.ChangeState(new TurnState());
            }
        }
    }

    public void DrawAbilityCard()
    {
        // Create the new card object & add it to the player's hand
        GameObject cardObj;
        playerHand.Add(cardObj = Instantiate(cardPrefab, playerHandObj.transform));

        // Draw a random ability card from all possible options
        cardObj.GetComponent<CardObj>().Init(abilityCards[rng.Next(abilityCards.Count)]);

        // Advances to the starting turn once the player has drawn 4 cards or if they have no cards left in the deck
        if (StateManager.Instance.GetCurrentState() is InitialDrawState)
        {
            Debug.Log(playerHandObj.GetComponentsInChildren<CardObj>().Length);
            if (playerHandObj.GetComponentsInChildren<CardObj>().Length > 3)
            {
                StateManager.Instance.ChangeState(new TurnState());
            }
        }
    }

    public void DrawScenarioCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        scenarioDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT SCENARIO";
        StateManager.Instance.ChangeState(new InitialDrawState());
    }

    public void PlayCard(GameObject cardObj, CardBase card)
    {
        pointSum += card.numberEffect;
        card.SpecialEffect();
        pointsUI.DisplayTotalPoints(pointSum);
        // Placeholder - card should be added to discard pile instead of being disabled
        cardObj.SetActive(false);
        playerHand.Remove(cardObj);
        cardsPlayedThisRound++;

        // Disable the player's hand if they have already played 3 cards
        if (cardsPlayedThisRound > 2)
        {
            playerHandObj.GetComponent<CanvasGroup>().interactable = false;
        }
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
                roundUI.UpdateTurnText(characterList[currentTurn]);
            }
            else
            {
                roundUI.EndRounds();
            }
        }
    }
}
