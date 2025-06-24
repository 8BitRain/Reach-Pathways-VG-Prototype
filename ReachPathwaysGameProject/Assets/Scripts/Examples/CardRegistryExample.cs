using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example script showing how to use the CardRegistry system
/// This script demonstrates various ways to work with card types and categories
/// </summary>
public class CardRegistryExample : MonoBehaviour
{
    [Header("Example Usage")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;
    
    void Start()
    {
        // Wait a frame to ensure CardRegistry is initialized
        Invoke(nameof(RunExamples), 0.1f);
    }
    
    void RunExamples()
    {
        Debug.Log("=== CardRegistry Examples ===");
        
        // Example 1: Get all Innovator cards
        List<Type> innovatorCards = CardRegistry.GetCardTypes(CardCategory.Innovator);
        Debug.Log($"Found {innovatorCards.Count} Innovator cards:");
        foreach (Type cardType in innovatorCards)
        {
            Debug.Log($"  - {cardType.Name}");
        }
        
        // Example 2: Get a random Innovator card
        Type randomInnovatorCard = CardRegistry.GetRandomCardType(CardCategory.Innovator);
        Debug.Log($"Random Innovator card: {randomInnovatorCard.Name}");
        
        // Example 3: Get multiple random cards without duplicates
        List<Type> randomCards = CardRegistry.GetRandomCardTypes(CardCategory.Strategist, 3);
        Debug.Log($"3 Random Strategist cards:");
        foreach (Type cardType in randomCards)
        {
            Debug.Log($"  - {cardType.Name}");
        }
        
        // Example 4: Get card counts for each category
        Debug.Log("Card counts by category:");
        foreach (CardCategory category in Enum.GetValues(typeof(CardCategory)))
        {
            int count = CardRegistry.GetCardCount(category);
            Debug.Log($"  {category}: {count} cards");
        }
        
        // Example 5: Create card instances (if you have a cardPrefab assigned)
        if (cardPrefab != null && cardParent != null)
        {
            CreateExampleCards();
        }
    }
    
    void CreateExampleCards()
    {
        Debug.Log("Creating example card instances...");
        
        // Create one card from each category
        CardCategory[] categories = { 
            CardCategory.Innovator, 
            CardCategory.Strategist, 
            CardCategory.Visionary, 
            CardCategory.Collaborator, 
            CardCategory.Communicator 
        };
        
        foreach (CardCategory category in categories)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            CardObj cardComponent = cardObj.GetComponent<CardObj>();
            
            if (cardComponent != null)
            {
                cardComponent.SetRandomCardFromCategory(category);
                Debug.Log($"Created {category} card: {cardComponent.card.cardName}");
            }
        }
    }
    
    // Example method you can call from buttons or other scripts
    public void CreateRandomInnovatorCard()
    {
        if (cardPrefab != null && cardParent != null)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            CardObj cardComponent = cardObj.GetComponent<CardObj>();
            
            if (cardComponent != null)
            {
                cardComponent.SetRandomCardFromCategory(CardCategory.Innovator);
                Debug.Log($"Created Innovator card: {cardComponent.card.cardName}");
            }
        }
    }
    
    // Example method to create a specific card type
    public void CreateSpecificCard(string cardTypeName)
    {
        // Find the card type by name
        List<Type> allCards = CardRegistry.GetCardTypes(CardCategory.All);
        Type targetCardType = allCards.Find(t => t.Name == cardTypeName);
        
        if (targetCardType != null && cardPrefab != null && cardParent != null)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            CardObj cardComponent = cardObj.GetComponent<CardObj>();
            
            if (cardComponent != null)
            {
                cardComponent.SetCardType(targetCardType);
                Debug.Log($"Created specific card: {cardComponent.card.cardName}");
            }
        }
        else
        {
            Debug.LogWarning($"Card type '{cardTypeName}' not found!");
        }
    }
}
