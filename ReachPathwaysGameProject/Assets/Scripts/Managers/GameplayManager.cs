using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using MemoryCards;
using AbilityCards;
using SupportCards;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using Unity.Mathematics;

public class GameplayManager : MonoBehaviour
{
    // #region
    public static GameplayManager Instance { get; private set; }
    public System.Random rng = new();

    [SerializeField]
    public GameObject cardPrefab, characterParent, actionsMenu, playerHandObj, playerDeckObj, scenarioDisplay, abilityDeck;
    [SerializeField]
    public RoundUI roundUI;
    [SerializeField]
    public PointsUI pointsUI;
    [SerializeField]
    public DiceUI diceUI;
    [SerializeField]
    public LogDisplay log;

    [SerializeField]
    private int totalRounds = 4;

    // Note that currentRound will start at 1 instead of 0 for text display purposes
    public int currentRound = 1;

    private int cardsPlayedThisTurn = 0;

    public int pointSum { get; private set; } = 0;

    public int currentTurn { get; private set; } = 0;

    public gameResult result { get; private set; }

    public bool endAllRounds { get; private set; }

    [SerializeField]
    public List<CharacterCard> characterList = new();
    [SerializeField]
    public CharacterCard playerCharacter;
    public bool isPlayerTurn = false;

    public List<Type> playerDeck = new();

    public Scenario currentScenario;

    public Dictionary<CharacterCard, List<GameObject>> hands = new();
    public Dictionary<CharacterCard, List<GameObject>> discards = new();
    public List<Type> revealedCards = new();

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

    // #endregion 

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

        scenarioDisplay.SetActive(false);

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
        InitializeHands();
        playerDeck.Add(innovatorCards[0]);
        playerDeck.Add(innovatorCards[6]);
        playerDeck.Add(strategistCards[2]);
        playerDeck.Add(innovatorCards[8]);
        // playerDeck.Add(strategistCards[6]);
        // playerDeck.Add(innovatorCards[7]);
    }

    public void SetScenario(Scenario scenario)
    {
        currentScenario = scenario;
        scenario.AssignObject(scenarioDisplay);
    }

    public void DrawScenarioCard()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.scenarioCard, transform.position);
        SetScenario(new Scenario(
            CardStat.Creativity,
            new CardStat[] { CardStat.Creativity, CardStat.Communication, CardStat.Awareness, CardStat.Integrity },
            new int[] { 30, 45, 65, 90 },
            new Dictionary<gameResult, int> {
                { gameResult.extraordinary, 100 },
                { gameResult.success, 80 },
                { gameResult.partialFailure, 61 } },
            scenarioDisplay));
        StateManager.Instance.ChangeState(new InitialDrawState());
    }

    public void DrawPlayerCard()
    {
        DrawState state = StateManager.Instance.GetCurrentState() as DrawState;
        if (!(playerDeck.Count > 0))
        {
            log.UpdateLog("Player is out of cards!");
        }
        else
        {
            if (state.cardsDrawn < state.limit)
            {
                // Create the new card object & save it in the player's hand
                GameObject cardObj;
                hands[playerCharacter].Add(cardObj = Instantiate(cardPrefab, playerHandObj.transform));
                // Draw a card & remove it from the player's deck
                int cardIndex = rng.Next(playerDeck.Count);
                cardObj.GetComponent<CardObj>().Init(playerDeck[cardIndex]);
                playerDeck.RemoveAt(cardIndex);

                // Enables drawing from the ability deck if the player ran out of cards in their deck on the initial draw
                if (playerDeck.Count < 1)
                {
                    playerDeckObj.GetComponent<Button>().interactable = false;
                    abilityDeck.GetComponent<Button>().interactable = true;
                }

                AudioManager.Instance.PlaySFX(AudioManager.Instance.drawCard, transform.position);

                state.cardsDrawn++;
                DrawAdvance(state);
            }
        }
    }

    public void DrawAbilityCard()
    {
        GameObject parent = (StateManager.Instance.GetCurrentState() is InitialDrawState || isPlayerTurn) ? playerHandObj : characterList[currentTurn].gameObject;

        // Special use case for HandDiscardDrawCard & DeckSearch card effects
        if (StateManager.Instance.GetCurrentState() is TurnState)
        {
            DrawCardBase(parent);
            return;
        }

        DrawState state = StateManager.Instance.GetCurrentState() as DrawState;
        // Sets the parent of the new card object to be the player's hand object if we are in the initial draw state or if it is the player's turn. Otherwise it will parent it directly to the character's object
        
        if (state.cardsDrawn < state.limit)
        {
            DrawCardBase(parent);
            state.cardsDrawn++;
            DrawAdvance(state);
        }
    }

    private void DrawCardBase(GameObject parent)
    {
        if (hands[characterList[currentTurn]].Count > 7)
        {
            log.UpdateLog($"{hands[characterList[currentTurn]]}'s hand is full, they cannot draw a new card.");
            return;
        }

        // Create the new card object & add it to the corresponding hand and object
        GameObject cardObj;
        hands[characterList[currentTurn]].Add(cardObj = Instantiate(cardPrefab, parent.gameObject.transform));

        // Draw a random ability card from all possible options, or from the revealed cards if they exist
        Type cardToDraw;
        if (revealedCards.Count > 0)
        {
            cardToDraw = revealedCards[0];
            revealedCards.Remove(cardToDraw);
        }
        else
        {
            cardToDraw = abilityCards[rng.Next(abilityCards.Count)];
        }
        cardObj.GetComponent<CardObj>().Init(cardToDraw);
        log.UpdateLog($"{characterList[currentTurn]} drew {cardObj.GetComponent<CardBase>().cardName}");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.drawCard, transform.position);
    }

    private void DrawAdvance(DrawState state)
    {
        // Moves to next state if the draw limit has been reached
        if (state.cardsDrawn >= state.limit)
        {
            switch (state)
            {
                case InitialDrawState:
                    StateManager.Instance.ChangeState(new DiceRollState());
                    break;
                case TurnEndDrawState:
                    if (!isPlayerTurn)
                    {
                        AdvanceTurn();
                    }
                    else
                    {
                        playerDeckObj.GetComponent<Button>().interactable = false;
                        abilityDeck.GetComponent<Button>().interactable = false;
                    }
                    break;
            }
        }
    }

    public void AddDiceValue(int diceValue)
    {
        pointSum += diceValue;
        pointsUI.DisplayTotalPoints(pointSum);

        StartCoroutine(StateManager.Instance.Delay(0.5f, done => { AudioManager.Instance.PlaySFX(AudioManager.Instance.diceRoll, transform.position); ; }));
    }

    public void PlayCard(GameObject cardObj, CardBase card)
    {
        pointSum += card.numberEffect;
        log.UpdateLog($"Playing card {card.cardName}, adding card value of {card.numberEffect}");
        if (card.stat == currentScenario.roundBonuses[currentRound - 1])
        {
            if (card.numberEffect < 0)
            {
                // If the card was negative, add twice the positive number to undo the previous addition and add the correct one
                pointSum += 2 * math.abs(card.numberEffect);
                log.UpdateLog("Applying negative -> positive effect to card due to round stat bonus");
            }
            pointSum++;
            log.UpdateLog("Adding round stat bonus of +1");
        }
        if (card.stat == currentScenario.domain)
        {
            pointSum++;
            log.UpdateLog("Adding scenario domain bonus of +1");
        }

        card.SpecialEffect();
        cardObj.SetActive(false);
        hands[characterList[currentTurn]].Remove(cardObj);
        discards[characterList[currentTurn]].Add(cardObj);
        cardsPlayedThisTurn++;
        pointsUI.DisplayTotalPoints(pointSum);

        if (isPlayerTurn)
        {
            // Disable the player's hand if they have already played 3 cards
            if (cardsPlayedThisTurn > 2)
            {
                playerHandObj.GetComponent<CanvasGroup>().interactable = false;
            }
        }
        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playCard, AudioManager.Instance.transform.position, AudioManager.Instance.gameObject, true);

    }

    public void AdvanceToDraw()
    {
        StateManager.Instance.ChangeState(new TurnEndDrawState());

        if (isPlayerTurn)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect, transform.position);
        }
    }

    public void AdvanceTurn()
    {
        // Increase the current turn then modulo by the number of players to reset to 0 once the end of the turn order is reached
        currentTurn = (currentTurn + 1) % characterList.Count;
        cardsPlayedThisTurn = 0;

        if (currentTurn == 0)
        {
            // Increment the round whenever the turn order wraps back around
            currentRound++;
            if (currentRound > totalRounds)
            {
                // End the game once the current round is incremented past the last round
                roundUI.EndRounds();
                if (pointSum >= currentScenario.finalThresholds[gameResult.extraordinary])
                {
                    result = gameResult.extraordinary;
                }
                else if (pointSum >= currentScenario.finalThresholds[gameResult.success])
                {
                    result = gameResult.success;
                }
                else if (pointSum >= currentScenario.finalThresholds[gameResult.partialFailure])
                {
                    result = gameResult.partialFailure;
                }
                else
                {
                    result = gameResult.failure;
                }
                log.UpdateLog("Game Result: " + result.ToString());
                return;
            }

            log.UpdateLog($"Checking if point total of {pointSum} passes the threshold of {currentScenario.roundThresholds[currentRound - 2]}");
            if (pointSum < currentScenario.roundThresholds[currentRound - 2])
            {
                log.UpdateLog("Round threshold was not met - players have to discard a card");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.roundFail, transform.position);
                foreach (KeyValuePair<CharacterCard, List<GameObject>> player in hands)
                {
                    if (player.Value.Count > 0)
                    {
                        GameObject cardToDiscard = player.Value[rng.Next(0, player.Value.Count)];
                        cardToDiscard.SetActive(false);
                        hands[player.Key].Remove(cardToDiscard);
                        discards[player.Key].Add(cardToDiscard);
                        log.UpdateLog($"Discarded {cardToDiscard.GetComponent<CardBase>().cardName} from player {player.Key}");
                    }
                }
            }
            // Go to dice roll state
            log.UpdateLog($"Beginning round {currentRound}");
            StateManager.Instance.ChangeState(new DiceRollState());
            return;
        }
        StateManager.Instance.ChangeState(new TurnState());
    }

    public void InitializeHands()
    {
        foreach (CharacterCard character in characterList)
        {
            if (character != playerCharacter)
            {
                GameObject obj = character.gameObject;

                List<GameObject> cardList = new();
                int count = 0;
                while (count < 4)
                {
                    GameObject cardObj;
                    cardList.Add(cardObj = Instantiate(cardPrefab, obj.transform));
                    cardObj.GetComponent<CardObj>().Init(abilityCards[rng.Next(abilityCards.Count)]);
                    count++;
                }
                hands.Add(character, cardList);
                discards.Add(character, new List<GameObject>());
            }
            else
            {
                hands.Add(playerCharacter, new List<GameObject>());
                discards.Add(playerCharacter, new List<GameObject>());
            }
        }
    }

    public void PlayCPUTurn()
    {
        CharacterCard character = characterList[currentTurn];
        CardObj cardObj = hands[character][rng.Next(hands[character].Count)].GetComponent<CardObj>();
        cardObj.PlayCard();
        if (rng.Next(0, 1) == 1)
        {
            // Randomly play a second card with a 50% chance
            cardObj.PlayCard();
        }

        StartCoroutine(StateManager.Instance.Delay(2f, done => { AdvanceToDraw(); }));
    }
}

public enum gameResult
{
    extraordinary, success, partialFailure, failure
}
