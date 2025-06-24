using System;
using System.Collections.Generic;
using UnityEngine;

public static class CardRegistry
{
    // Dictionary to store card types by category
    private static Dictionary<CardCategory, List<Type>> cardTypesByCategory = new Dictionary<CardCategory, List<Type>>();
    
    // Initialize the registry with all card types
    static CardRegistry()
    {
        InitializeCardTypes();
    }
    
    private static void InitializeCardTypes()
    {
        // Innovator Cards
        var innovatorCards = new List<Type>
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
        var strategistCards = new List<Type>
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
        var visionaryCards = new List<Type>
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
        var collaboratorCards = new List<Type>
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
        var communicatorCards = new List<Type>
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
        
        // Add all categories to the dictionary
        cardTypesByCategory[CardCategory.Innovator] = innovatorCards;
        cardTypesByCategory[CardCategory.Strategist] = strategistCards;
        cardTypesByCategory[CardCategory.Visionary] = visionaryCards;
        cardTypesByCategory[CardCategory.Collaborator] = collaboratorCards;
        cardTypesByCategory[CardCategory.Communicator] = communicatorCards;
        
        // Create an "All" category that contains all cards
        var allCards = new List<Type>();
        foreach (var cardList in cardTypesByCategory.Values)
        {
            allCards.AddRange(cardList);
        }
        cardTypesByCategory[CardCategory.All] = allCards;
    }
    
    /// <summary>
    /// Get all card types for a specific category
    /// </summary>
    /// <param name="category">The card category to retrieve</param>
    /// <returns>List of card types in the specified category</returns>
    public static List<Type> GetCardTypes(CardCategory category)
    {
        if (cardTypesByCategory.ContainsKey(category))
        {
            return new List<Type>(cardTypesByCategory[category]); // Return a copy to prevent modification
        }
        
        Debug.LogWarning($"Card category {category} not found in registry!");
        return new List<Type>();
    }
    
    /// <summary>
    /// Get a random card type from a specific category
    /// </summary>
    /// <param name="category">The card category to select from</param>
    /// <returns>A random card type from the specified category</returns>
    public static Type GetRandomCardType(CardCategory category)
    {
        var cardTypes = GetCardTypes(category);
        if (cardTypes.Count == 0)
        {
            Debug.LogError($"No cards found for category {category}!");
            return null;
        }
        
        int randomIndex = UnityEngine.Random.Range(0, cardTypes.Count);
        return cardTypes[randomIndex];
    }
    
    /// <summary>
    /// Get multiple random card types from a specific category (without duplicates)
    /// </summary>
    /// <param name="category">The card category to select from</param>
    /// <param name="count">Number of card types to select</param>
    /// <returns>List of random card types from the specified category</returns>
    public static List<Type> GetRandomCardTypes(CardCategory category, int count)
    {
        var availableCards = GetCardTypes(category);
        var selectedCards = new List<Type>();
        
        if (count > availableCards.Count)
        {
            Debug.LogWarning($"Requested {count} cards but only {availableCards.Count} available in {category} category. Returning all available cards.");
            return availableCards;
        }
        
        for (int i = 0; i < count; i++)
        {
            if (availableCards.Count == 0) break;
            
            int randomIndex = UnityEngine.Random.Range(0, availableCards.Count);
            selectedCards.Add(availableCards[randomIndex]);
            availableCards.RemoveAt(randomIndex); // Remove to prevent duplicates
        }
        
        return selectedCards;
    }
    
    /// <summary>
    /// Create an instance of a card type and add it to a GameObject
    /// </summary>
    /// <param name="cardType">The type of card to create</param>
    /// <param name="gameObject">The GameObject to add the card component to</param>
    /// <returns>The created CardBase component</returns>
    public static CardBase CreateCardInstance(Type cardType, GameObject gameObject)
    {
        if (cardType == null || !cardType.IsSubclassOf(typeof(CardBase)))
        {
            Debug.LogError($"Invalid card type: {cardType}. Must be a subclass of CardBase.");
            return null;
        }
        
        return (CardBase)gameObject.AddComponent(cardType);
    }
    
    /// <summary>
    /// Get the count of cards in a specific category
    /// </summary>
    /// <param name="category">The card category to count</param>
    /// <returns>Number of cards in the specified category</returns>
    public static int GetCardCount(CardCategory category)
    {
        return GetCardTypes(category).Count;
    }
}
