# Card Registry System Setup Guide

## Current Status

The Card Registry system has been created with the following files:

### ✅ Working Files (No Compilation Issues)
- `Assets/Scripts/Core/CardCategory.cs` - Enum defining card categories
- `Assets/Scripts/Managers/CardRegistry.cs` - Main registry system
- `Assets/Scripts/UI/CardObj.cs` - Current working version with all card types listed
- `Assets/Scripts/Managers/GameplayManager.cs` - Updated with basic functionality
- `Assets/Scripts/Examples/CardRegistryExample.cs` - Example usage (may have compilation issues until Unity compiles CardRegistry)

### 🔄 Enhanced Files (Use After Unity Compiles Everything)
- `Assets/Scripts/UI/CardObjEnhanced.cs` - Full-featured version using CardRegistry

## Setup Steps

### Step 1: Let Unity Compile
1. Save all files and let Unity compile the scripts
2. Check the Console for any remaining compilation errors
3. If there are errors, they should resolve once all scripts are compiled in the correct order

### Step 2: Test Current System
The current `CardObj.cs` should work immediately and provides:
- Random card selection from all 51 available cards
- Ability to set specific card types programmatically
- All card types are listed directly in the code

### Step 3: Upgrade to Enhanced System (Optional)
Once Unity compiles successfully:

1. **Replace CardObj with CardObjEnhanced:**
   - In your card prefabs, replace the `CardObj` component with `CardObjEnhanced`
   - Or rename `CardObjEnhanced.cs` to `CardObj.cs` and delete the old one

2. **Update GameplayManager:**
   - Uncomment the CardRegistry usage examples
   - Add category-specific drawing methods

## Current Functionality

### What Works Right Now:
```csharp
// In GameplayManager
GameplayManager.Instance.DrawSkillCard(); // Draws random card from all cards
GameplayManager.Instance.DrawSpecificSkillCard(typeof(EurekaCard)); // Draws specific card

// In CardObj
CardObj cardObj = GetComponent<CardObj>();
cardObj.SetCardType(typeof(EurekaCard)); // Set specific card type
```

### What Will Work After Full Setup:
```csharp
// Category-based card drawing
GameplayManager.Instance.DrawSkillCard(CardCategory.Innovator);

// Enhanced CardObj features
CardObjEnhanced cardObj = GetComponent<CardObjEnhanced>();
cardObj.SetRandomCardFromCategory(CardCategory.Strategist);
cardObj.SetCardByName("EurekaCard");

// Registry queries
List<Type> innovatorCards = CardRegistry.GetCardTypes(CardCategory.Innovator);
Type randomCard = CardRegistry.GetRandomCardType(CardCategory.Visionary);
```

## Card Categories

The system organizes your 51 cards into these categories:

- **Innovator** (9 cards): EurekaCard, UnconventionalHackCard, AdaptTheWorldCard, etc.
- **Strategist** (9 cards): TimedJustRightCard, AllInOnePieceCard, BreatheInBreatheOutCard, etc.
- **Visionary** (11 cards): APinchOfPunctualityCard, TheGiftOfAVisionCard, PromisesPromisesCard, etc.
- **Collaborator** (11 cards): AHelpingHand, WeListenAndWeDontJudgeCard, TakingInitiativeCard, etc.
- **Communicator** (11 cards): AllsWellThatEndsWellCard, FruitfulTruthsCard, TalkOfTheTownCard, etc.
- **All** (51 cards): Contains all cards from all categories

## Usage Examples

### Example 1: Character-Specific Card Types
```csharp
public class Character : MonoBehaviour
{
    public CardCategory preferredCategory = CardCategory.Innovator;
    
    void Start()
    {
        // Once CardRegistry is available, you can do:
        // GameplayManager.Instance.DrawSkillCard(preferredCategory);
    }
}
```

### Example 2: Balanced Deck Creation
```csharp
public void CreateBalancedHand()
{
    // Draw one card from each category
    GameplayManager.Instance.DrawSpecificSkillCard(typeof(EurekaCard)); // Innovator
    GameplayManager.Instance.DrawSpecificSkillCard(typeof(TimedJustRightCard)); // Strategist
    GameplayManager.Instance.DrawSpecificSkillCard(typeof(TheGiftOfAVisionCard)); // Visionary
}
```

### Example 3: Random Card Selection
```csharp
public void DrawRandomCards()
{
    // Current system - draws from all cards
    for (int i = 0; i < 5; i++)
    {
        GameplayManager.Instance.DrawSkillCard();
    }
}
```

## Troubleshooting

### If you get compilation errors:
1. Make sure all files are saved
2. Let Unity finish compiling (check the progress bar at the bottom)
3. Check that `CardCategory.cs` compiles first (it's in the Core folder)
4. If issues persist, restart Unity

### If CardRegistry methods aren't available:
- Use the current `CardObj.cs` system which works immediately
- The enhanced features will be available once Unity compiles everything

## Benefits of This System

1. **Immediate Functionality**: Current system works right away
2. **Organized Card Management**: Cards are categorized by type
3. **Flexible Selection**: Choose specific cards or random from categories
4. **Easy Extension**: Add new cards by updating the lists
5. **Type Safety**: Uses C# Type system for compile-time checking
6. **No Hardcoding**: Eliminates the need to hardcode specific card types

## Next Steps

1. Test the current system to make sure cards are being created properly
2. Once Unity compiles successfully, consider upgrading to the enhanced system
3. Implement character-specific card preferences using categories
4. Create balanced deck systems using the category-based selection
