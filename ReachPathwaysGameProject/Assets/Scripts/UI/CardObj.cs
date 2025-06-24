using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CardObj : MonoBehaviour
{
    [SerializeField]
    public CardBase card;

    void Awake()
    {
        // Create a random card from all available cards
        CreateRandomCard();
        
        if (card == null)
        {
            Debug.LogError("Failed to create card instance!");
        }
    }
    
    /// <summary>
    /// Create a random card from all available card types
    /// </summary>
    private void CreateRandomCard()
    {
        // List of all card types - you can modify this list as needed
        Type[] allCardTypes = {
            // Innovator Cards
            typeof(EurekaCard),
            typeof(UnconventionalHackCard),
            typeof(AdaptTheWorldCard),
            typeof(ArtTherapyCard),
            typeof(WeatherTheBrainstormCard),
            typeof(BlueElectricityWhiteSmokeCard),
            typeof(MissingEyebrowsCard),
            typeof(StuckInARutCard),
            typeof(WatchItBurnCard),
            
            // Strategist Cards
            typeof(TimedJustRightCard),
            typeof(AllInOnePieceCard),
            typeof(BreatheInBreatheOutCard),
            typeof(PerceivedRisksCard),
            typeof(RestAndRecuperateCard),
            typeof(FriendInNeedCard),
            typeof(EmotionalOutburstCard),
            typeof(TearToPiecesCard),
            typeof(LackOfAwarenessCard),
            
            // Visionary Cards
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
            typeof(AstronomicalRecalculationCard),
            
            // Collaborator Cards
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
            typeof(TooManyEggsForOneBasketCard),
            
            // Communicator Cards
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
        
        // Select a random card type
        int randomIndex = UnityEngine.Random.Range(0, allCardTypes.Length);
        Type selectedCardType = allCardTypes[randomIndex];
        
        // Create the card instance
        card = (CardBase)gameObject.AddComponent(selectedCardType);
    }
    
    /// <summary>
    /// Set a specific card type for this CardObj
    /// </summary>
    /// <param name="cardType">The card type to set</param>
    public void SetCardType(Type cardType)
    {
        if (card != null)
        {
            DestroyImmediate(card);
        }
        
        if (cardType != null && cardType.IsSubclassOf(typeof(CardBase)))
        {
            card = (CardBase)gameObject.AddComponent(cardType);
        }
        else
        {
            Debug.LogError($"Invalid card type: {cardType}. Must be a subclass of CardBase.");
        }
    }

    public void PlayCard()
    {
        GameplayManager.Instance.AdvanceTurn();
        gameObject.SetActive(false);
    }
}
