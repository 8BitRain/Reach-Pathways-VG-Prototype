# Card Registry System Usage Guide

The CardRegistry system provides a centralized way to manage and instantiate card types in your game. This system allows you to organize cards by categories and easily create instances of specific card types or random cards from categories.

## Overview

The system consists of:
- **CardRegistry**: A static class that manages all card types
- **CardCategory**: An enum defining different card categories
- **Updated CardObj**: Now supports category-based and specific card instantiation

## Card Categories

The system organizes cards into the following categories:

- **Innovator**: 9 cards (EurekaCard, UnconventionalHackCard, etc.)
- **Strategist**: 9 cards (TimedJustRightCard, AllInOnePieceCard, etc.)
- **Visionary**: 11 cards (APinchOfPunctualityCard, TheGiftOfAVisionCard, etc.)
- **Collaborator**: 11 cards (AHelpingHand, WeListenAndWeDontJudgeCard, etc.)
- **Communicator**: 11 cards (AllsWellThatEndsWellCard, FruitfulTruthsCard, etc.)
- **All**: Contains all cards from all categories

## Basic Usage

### 1. Get All Cards from a Category

```csharp
List<Type> innovatorCards = CardRegistry.GetCardTypes(CardCategory.Innovator);
Debug.Log($"Found {innovatorCards.Count} Innovator cards");
```

### 2. Get a Random Card from a Category

```csharp
Type randomInnovatorCard = CardRegistry.GetRandomCardType(CardCategory.Innovator);
Debug.Log($"Random Innovator card: {randomInnovatorCard.Name}");
```

### 3. Get Multiple Random Cards (No Duplicates)

```csharp
List<Type> randomCards = CardRegistry.GetRandomCardTypes(CardCategory.Strategist, 3);
foreach (Type cardType in randomCards)
{
    Debug.Log($"Selected card: {cardType.Name}");
}
```

### 4. Create Card Instances

```csharp
// Create a specific card type
Type cardType = typeof(EurekaCard);
CardBase card = CardRegistry.CreateCardInstance(cardType, gameObject);

// Or get a random type first
Type randomType = CardRegistry.GetRandomCardType(CardCategory.Innovator);
CardBase randomCard = CardRegistry.CreateCardInstance(randomType, gameObject);
```

### 5. Get Card Count for a Category

```csharp
int innovatorCount = CardRegistry.GetCardCount(CardCategory.Innovator);
Debug.Log($"There are {innovatorCount} Innovator cards");
```

## Using with CardObj

The CardObj class has been updated to work with the CardRegistry system:

### In the Inspector

- **Card Category**: Set which category to draw random cards from
- **Specific Card Type**: Optionally specify a specific card type

### Programmatically

```csharp
CardObj cardObj = GetComponent<CardObj>();

// Set a random card from a specific category
cardObj.SetRandomCardFromCategory(CardCategory.Innovator);

// Set a specific card type
cardObj.SetCardType(typeof(EurekaCard));
```

## Integration with GameplayManager

The GameplayManager now has an overloaded method to draw cards from specific categories:

```csharp
// Draw any card (original behavior)
GameplayManager.Instance.DrawSkillCard();

// Draw a card from a specific category
GameplayManager.Instance.DrawSkillCard(CardCategory.Innovator);
```

## Example Scenarios

### Scenario 1: Character-Specific Decks

```csharp
// Each character could have their own preferred card category
public class Character : MonoBehaviour
{
    public CardCategory preferredCardCategory;
    
    public void DrawCard()
    {
        GameplayManager.Instance.DrawSkillCard(preferredCardCategory);
    }
}
```

### Scenario 2: Balanced Hand Creation

```csharp
// Create a balanced hand with cards from different categories
public void CreateBalancedHand()
{
    CardCategory[] categories = { 
        CardCategory.Innovator, 
        CardCategory.Strategist, 
        CardCategory.Visionary 
    };
    
    foreach (CardCategory category in categories)
    {
        GameplayManager.Instance.DrawSkillCard(category);
    }
}
```

### Scenario 3: Deck Building

```csharp
// Build a custom deck with specific card distributions
public List<Type> BuildCustomDeck()
{
    List<Type> customDeck = new List<Type>();
    
    // Add 5 random Innovator cards
    customDeck.AddRange(CardRegistry.GetRandomCardTypes(CardCategory.Innovator, 5));
    
    // Add 3 random Strategist cards
    customDeck.AddRange(CardRegistry.GetRandomCardTypes(CardCategory.Strategist, 3));
    
    // Add 2 specific cards
    customDeck.Add(typeof(EurekaCard));
    customDeck.Add(typeof(TimedJustRightCard));
    
    return customDeck;
}
```

## Benefits

1. **Centralized Management**: All card types are managed in one place
2. **Easy Categorization**: Cards are automatically organized by type
3. **Flexible Instantiation**: Create specific cards or random cards from categories
4. **No Duplicates**: The system can ensure no duplicate cards when drawing multiple
5. **Extensible**: Easy to add new cards or categories
6. **Type Safety**: Uses C# Type system for compile-time safety

## Adding New Cards

To add new cards to the system:

1. Create your new card class extending CardBase
2. Add the card type to the appropriate category list in CardRegistry.InitializeCardTypes()
3. The card will automatically be available through all CardRegistry methods

## Troubleshooting

If you encounter compilation errors:
1. Make sure all scripts are in the correct folders
2. Let Unity recompile all scripts
3. Check that your card classes properly extend CardBase
4. Ensure the CardRegistry.cs file is compiled before other scripts that use it
