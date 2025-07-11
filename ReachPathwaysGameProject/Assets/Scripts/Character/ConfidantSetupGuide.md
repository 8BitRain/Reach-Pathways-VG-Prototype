# Confidant System Setup Guide

This guide explains how to set up a simple Confidant system that works reliably with YarnSpinner using traditional variable storage.

## Components Overview

### 1. Confidant.cs
- Stores confidant data (name, rank, unlock status) directly in the component
- Provides YarnCommand methods that update Yarn variables automatically
- Uses traditional variable storage to avoid source generator compatibility issues

### 2. Updated Yarn Script (Confidant.yarn)
- Uses standard Yarn variables ($conRank, $conName, $conUnlocked)
- Demonstrates conditional dialogue based on confidant status
- Shows interactive options for rank management
- Features a looping menu system that returns to options after each action
- Includes save/load options for persistent data

## Setup Instructions

### Step 1: Attach the Confidant Component
1. Select your Dialogue System GameObject (or confidant character GameObject)
2. Add the `Confidant` component
3. Configure the confidant settings in the inspector:
   - **Confidant Name**: The name of this confidant
   - **Con Rank**: Starting rank (default: 0)
   - **Is Unlocked**: Whether this confidant is available for interaction
4. Assign the **Dialogue Runner** reference

### Step 2: Configure Your Yarn Project
Your Yarn scripts can now use these commands and variables:

#### Available YarnCommands (Actions):
- `<<IncreaseRank GameObjectName>>` - Increases rank by 1 and updates $conRank
- `<<SetRank GameObjectName X>>` - Sets rank to specific value X and updates $conRank
- `<<UnlockConfidant GameObjectName>>` - Unlocks this confidant and updates $conUnlocked
- `<<LockConfidant GameObjectName>>` - Locks this confidant and updates $conUnlocked
- `<<AdvanceTimeSlot GameObjectName X>>` - Changes the time/date by X amount based on the player's decision (1 slot = rest | 2 slots = Skill-based activity | 2 slots = confidant interaction | 5 slots = Scenario (card game) | 5 slots = major confidant event)

**Important**: You must specify the GameObject name that has the Confidant component when calling commands.

#### Available Yarn Variables:
- `$conRank` - Current confidant rank (number)
- `$conName` - Confidant name (string)
- `$conUnlocked` - Whether confidant is unlocked (boolean)

**Note**: This approach uses traditional Yarn variables that are automatically synchronized with the C# component data, avoiding source generator compatibility issues.

### Step 3: Example Yarn Script Usage

```yarn
title: MyConfidant
---
<<declare $player = "PlayerName">>
<<declare $conRank = 0>>
<<declare $conName = "DefaultConfidant">>
<<declare $conUnlocked = true>>

PersonA: Hello, {$player}!
PersonA: I'm {$conName}.

<<if $conUnlocked>>
    PersonA: Our current relationship rank is {$conRank}.
    
    -> Increase our rank
        <<IncreaseRank>>
        PersonA: Great! We're now rank {$conRank}!
    -> Set rank to 5
        <<SetRank 5>>
        PersonA: Rank set to {$conRank}.
    -> Check if we're high rank
        <<if $conRank >= 5>>
            PersonA: We have a strong relationship!
        <<else>>
            PersonA: We should spend more time together.
        <<endif>>
<<else>>
    PersonA: I can't talk to you right now.
    -> Unlock relationship
        <<UnlockConfidant>>
        PersonA: Okay, now we can be friends!
<<endif>>
===
```

## Benefits of This Approach

1. **Compatibility**: No source generator issues - uses standard YarnSpinner features
2. **Simplicity**: Traditional variable approach that's well-documented
3. **Automatic Sync**: C# component data automatically updates Yarn variables
4. **Inspector Friendly**: All data visible and editable in Unity Inspector
5. **Multiple Confidants**: Each GameObject can have its own Confidant component
6. **Reliable**: Uses proven YarnSpinner patterns

## Adding More Confidants

To add more confidants:
1. Create a new GameObject for each confidant
2. Attach the `Confidant` component
3. Set unique confidant names
4. Reference the same DialogueRunner
5. Create separate Yarn nodes for each confidant or use conditional logic

## How It Works

1. The Confidant component stores data in C# fields
2. When commands are called, the component updates both its internal data and the Yarn variables
3. Yarn scripts use the standard variables ($conRank, $conName, $conUnlocked)
4. The system automatically keeps C# data and Yarn variables synchronized

## Creating Looping Menus

The updated Yarn script demonstrates how to create a persistent menu system:

1. **Main Node**: Initial dialogue and setup
2. **Menu Node**: Separate node with options that loops back to itself
3. **Jump Commands**: Use `<<jump NodeName>>` to move between nodes
4. **Exit Option**: Include a "Goodbye" option that doesn't jump back

```yarn
title: MainNode
---
PersonA: Hello!
<<jump MenuNode>>
===

title: MenuNode
---
PersonA: What would you like to do?
-> Option 1
    PersonA: You chose option 1!
    <<jump MenuNode>>  // Returns to menu
-> Option 2
    PersonA: You chose option 2!
    <<jump MenuNode>>  // Returns to menu
-> Goodbye
    PersonA: See you later!  // Ends dialogue
===
```

## Save/Load System

The Confidant system now includes automatic save/load functionality to persist data between game sessions.

### How It Works

1. **Automatic Saving**: Data is saved automatically when:
   - Any confidant action is performed (rank changes, lock/unlock)
   - The application loses focus or is paused
   - The GameObject is destroyed
   - The application quits

2. **Automatic Loading**: Data is loaded automatically when:
   - The component starts (if Auto Load is enabled)
   - You can also manually load with the LoadData command

3. **Storage Method**: Uses Unity's PlayerPrefs with JSON serialization
   - Each confidant is saved with a unique key: `Confidant_{confidantName}`
   - Data persists across game sessions and builds

### Save/Load Settings

In the Inspector, you can configure:
- **Auto Save**: Automatically saves after each change (recommended: true)
- **Auto Load**: Automatically loads saved data on Start (recommended: true)

### Available Save/Load Commands

- `<<SaveData GameObjectName>>` - Manually save current data
- `<<LoadData GameObjectName>>` - Manually load saved data
- `<<ResetConfidant GameObjectName>>` - Reset to default values and save

### Example Usage in Yarn

```yarn
-> Save my progress
    <<SaveData Confidant>>
    PersonA: I've saved our relationship progress!
-> Reset everything
    <<ResetConfidant Confidant>>
    PersonA: I've reset our relationship to the beginning.
```

### Save Data Structure

The system saves:
- Confidant name
- Current rank
- Unlock status

### Managing Save Data

From C# code, you can:
```csharp
// Manual save/load
confidant.SaveConfidantData();
confidant.LoadConfidantData();

// Delete save data
confidant.DeleteSaveData();

// Check if save data exists
string saveKey = $"Confidant_{confidantName}";
bool hasSaveData = PlayerPrefs.HasKey(saveKey);
```

## Extending the System

To add new functionality, add methods to the Confidant class:

```csharp
[YarnCommand]
public void ResetRank()
{
    conRank = 0;
    UpdateYarnVariables();
}

[YarnCommand]
public void SetMaxRank()
{
    conRank = 10;
    UpdateYarnVariables();
}
```

Then use them in Yarn scripts:
```yarn
<<ResetRank Confidant>>
PersonA: I've reset our rank to {$conRank}.
```

**Remember**: Always include the GameObject name when calling YarnCommands!

## Troubleshooting

- Make sure the DialogueRunner reference is assigned
- Ensure your Yarn script declares the variables at the top
- The component automatically updates variables when commands are called
- Check the console for debug messages when ranks change
