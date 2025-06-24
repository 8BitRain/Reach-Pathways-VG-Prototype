using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Enhanced version of CardObj that uses the CardRegistry system
/// Use this version once Unity has compiled all the CardRegistry scripts
/// </summary>
public class CardObjEnhanced : MonoBehaviour
{
    [SerializeField]
    public CardBase card;
    
    [SerializeField]
    public CardCategory cardCategory = CardCategory.All; // Default to all cards
    
    [SerializeField]
    public bool useSpecificCard = false;
    
    [SerializeField]
    public string specificCardTypeName = ""; // Name of specific card type to use

    void Awake()
    {
        CreateCard();
        
        if (card == null)
        {
            Debug.LogError("Failed to create card instance!");
        }
    }
    
    private void CreateCard()
    {
        if (useSpecificCard && !string.IsNullOrEmpty(specificCardTypeName))
        {
            // Try to create a specific card type by name
            CreateSpecificCardByName(specificCardTypeName);
        }
        else
        {
            // Create a random card from the specified category
            Type randomCardType = CardRegistry.GetRandomCardType(cardCategory);
            card = CardRegistry.CreateCardInstance(randomCardType, gameObject);
        }
    }
    
    private void CreateSpecificCardByName(string cardTypeName)
    {
        // Find the card type by name from all available cards
        var allCards = CardRegistry.GetCardTypes(CardCategory.All);
        Type targetCardType = allCards.Find(t => t.Name.Equals(cardTypeName, StringComparison.OrdinalIgnoreCase));
        
        if (targetCardType != null)
        {
            card = CardRegistry.CreateCardInstance(targetCardType, gameObject);
        }
        else
        {
            Debug.LogWarning($"Card type '{cardTypeName}' not found! Creating random card instead.");
            Type randomCardType = CardRegistry.GetRandomCardType(cardCategory);
            card = CardRegistry.CreateCardInstance(randomCardType, gameObject);
        }
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
        
        card = CardRegistry.CreateCardInstance(cardType, gameObject);
    }
    
    /// <summary>
    /// Set a random card from a specific category
    /// </summary>
    /// <param name="category">The category to select from</param>
    public void SetRandomCardFromCategory(CardCategory category)
    {
        if (card != null)
        {
            DestroyImmediate(card);
        }
        
        cardCategory = category;
        useSpecificCard = false;
        
        Type randomCardType = CardRegistry.GetRandomCardType(category);
        card = CardRegistry.CreateCardInstance(randomCardType, gameObject);
    }
    
    /// <summary>
    /// Set a specific card by its class name
    /// </summary>
    /// <param name="cardTypeName">The name of the card class</param>
    public void SetCardByName(string cardTypeName)
    {
        if (card != null)
        {
            DestroyImmediate(card);
        }
        
        specificCardTypeName = cardTypeName;
        useSpecificCard = true;
        
        CreateSpecificCardByName(cardTypeName);
    }
    
    /// <summary>
    /// Get information about the current card
    /// </summary>
    /// <returns>String with card information</returns>
    public string GetCardInfo()
    {
        if (card == null) return "No card assigned";
        
        return $"Card: {card.cardName}\nDescription: {card.description}\nEffect: {card.numberEffect}";
    }

    public void PlayCard()
    {
        if (GameplayManager.Instance != null)
        {
            GameplayManager.Instance.AdvanceTurn();
        }
        gameObject.SetActive(false);
    }
}
