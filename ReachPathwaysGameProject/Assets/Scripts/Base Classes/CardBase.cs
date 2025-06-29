using System.Collections;
using System.Collections.Generic;
using MemoryCards;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    public abstract string cardName { get; }
    public abstract string description { get; }
    public abstract CardStat stat { get; }
    public abstract int numberEffect
    { get; }
    public abstract void SpecialEffect();
}

public enum CardStat
{
    Creativity, Awareness, Integrity, Teamwork, Communication
}

namespace MemoryCards
{
    // Classes to group cards with shared effects
    public abstract class NeutralCard : CardBase
    {
        public override int numberEffect => 1;
        public override void SpecialEffect()
        {
            // No effect
            return;
        }
    }

    public abstract class GiveCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Give card method
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class DeckSearchCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Look through the deck, and choose any one card before reshuffling
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class HandDiscardDrawCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Discard as many cards in your hand as you like, then draw that many cards
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class PartialFailureToSuccessCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Partial failure becomes a success
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class FailureToSuccessCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Failure becomes a success
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class AskTeammateForCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Ask a teammate for a card from their hand
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class ShuffleDeckCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Shuffle the deck
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class ShareTopThreeCards : CardBase
    {
        public override void SpecialEffect()
        {
            // Look at the top three cards in the deck and share with your teammates
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public abstract class RecoverDiscardedCard : CardBase
    {
        public override void SpecialEffect()
        {
            // Recover any discarded card
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    // Creativity Cards
    public class EurekaCard : DeckSearchCard
    {
        public override string cardName => "EUREKA!";
        public override string description => "You recall a time you successfully worked through a problem with a clever solution.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => 2;
    }

    public class UnconventionalHackCard : CardBase
    {
        public override string cardName => "Unconventional Hack";
        public override string description => "You recall a time your unconventional solution saved Kaharaba's main building.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => 2;
        public override void SpecialEffect()
        {
            // Draw 2 cards after playing.
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class AdaptTheWorldCard : GiveCard
    {
        public override string cardName => "Adapt the World";
        public override string description => "You recall a time you reworked an existing solution to fix an age old problem.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => 2;
    }

    public class ArtTherapyCard : NeutralCard
    {
        public override string cardName => "Art Therapy";
        public override string description => "You recall a time you used a creative medium to revitalize your team.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => 1;
    }

    public class WeatherTheBrainstormCard : NeutralCard
    {
        public override string cardName => "Weather the Brainstorm";
        public override string description => "You recall a time your brainstorm produced a number of ideas, but you couldn't settle on one.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => 1;
    }

    public class BlueElectricityWhiteSmokeCard : NeutralCard
    {
        public override string cardName => "Blue Electricity, White Smoke";
        public override string description => "You recall a time your invention worked before fizzling out in a dramatic display of smoke.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => 1;
    }

    public class MissingEyebrowsCard : PartialFailureToSuccessCard
    {
        public override string cardName => "Missing Eyebrows";
        public override string description => "You recall a time your invention backfired and hurt you badly.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => -1;
    }

    public class StuckInARutCard : HandDiscardDrawCard
    {
        public override string cardName => "Stuck In A Rut";
        public override string description => "You recall a time you could not shake the brain fog. The solution remained just beyond the haze.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => -1;
    }

    public class WatchItBurnCard : AskTeammateForCard
    {
        public override string cardName => "Watch It Burn";
        public override string description => "You recall a time the idea you created caused exhaustive arguments between the guild members.";
        public override CardStat stat => CardStat.Creativity;
        public override int numberEffect => -1;
    }

    // Awareness Cards
    public class TimedJustRightCard : DeckSearchCard
    {
        public override string cardName => "Timed Just Right";
        public override string description => "You recall a time you crafted a work schedule for your team which helped the guild run smoothly.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => 2;
    }

    public class AllInOnePieceCard : GiveCard
    {
        public override string cardName => "All in One Piece";
        public override string description => "You recall a time the pieces of your plan all came together, and it went perfectly.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => 2;
    }

    public class BreatheInBreatheOutCard : ShareTopThreeCards
    {
        public override string cardName => "Breathe In, Breathe Out";
        public override string description => "You recall a time you led your team in wellness exercises to focus them, brighten their spirits, and ease their stress.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => 2;
    }

    public class PerceivedRisksCard : NeutralCard
    {
        public override string cardName => "Perceived Risks";
        public override string description => "You recall a time you noticed the potentially dangerous risks in a prospective plan.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => 1;
    }

    public class RestAndRecuperateCard : NeutralCard
    {
        public override string cardName => "Rest and Recuperate";
        public override string description => "You recall a time you perceived the stress in your body before an important meeting and rested in Afya.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => 1;
    }

    public class FriendInNeedCard : NeutralCard
    {
        public override string cardName => "Friend in Need";
        public override string description => "You recall a time a guild member accompanied you on a nature walk after noticing you ignoring your own stress.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => 1;
    }

    public class EmotionalOutburstCard : ShuffleDeckCard
    {
        public override string cardName => "Emotional Outburst";
        public override string description => "You recall a time the frustration got to you, and you shouted at your team.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => -1;
    }

    public class TearToPiecesCard : RecoverDiscardedCard
    {
        public override string cardName => "Tear to Pieces";
        public override string description => "You recall a time you did not notice the escalating tension in the guild, and stood by as it boiled over into an argument.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => -1;
    }

    public class LackOfAwarenessCard : PartialFailureToSuccessCard
    {
        public override string cardName => "Lack of Awareness";
        public override string description => "You recall a time you overlooked the growing stress in the guild and morale took a massive hit.";
        public override CardStat stat => CardStat.Awareness;
        public override int numberEffect => -1;
    }

    // Integrity Cards
    public class APinchOfPunctualityCard : GiveCard
    {
        public override string cardName => "A Pinch of Punctuality";
        public override string description => "You recall a time when you arrived on time for a meeting, ready to take on the world.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 2;
    }

    public class TheGiftOfAVisionCard : DeckSearchCard
    {
        public override string cardName => "The Gift of a Vision";
        public override string description => "You recall a time when you saw your dreams through, and it came out exactly as you hoped it could have.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 2;
    }

    public class PromisesPromisesCard : RecoverDiscardedCard
    {
        public override string cardName => "Promises, Promises";
        public override string description => "You recall a time when you followed through with a commitment, securing the trust of your team.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 2;
        public override void SpecialEffect()
        {
            // Recover a random card from the discard pile.
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class ElephantInTheRoomCard : NeutralCard
    {
        public override string cardName => "Elephant in the Room";
        public override string description => "You recall a time when you needed to address unethical behavior of a colleague in a constructive manner.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 1;
    }

    public class AdvocationPracticesCard : NeutralCard
    {
        public override string cardName => "Advocation Practices";
        public override string description => "You recall a time you spoke up about utilizing ethical practices during a high-stakes project.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 1;
    }

    public class DelayedAccountabilityCard : NeutralCard
    {
        public override string cardName => "Delayed Accountability";
        public override string description => "You recall a time when your lack of accountability led to a major project delay.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 1;
    }

    public class TheBiggerPictureCard : NeutralCard
    {
        public override string cardName => "The Bigger Picture";
        public override string description => "You recall a time when you were trying to create something but couldn't wrap your head around it, until stepping away to unveil crucial revelations.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 1;
    }

    public class SuperSonicPotentialCard : NeutralCard
    {
        public override string cardName => "Super-Sonic Potential";
        public override string description => "You recall a time when you brought an idea to the table, and gathered an extensive team to take it on.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => 1;
    }

    public class OverestimatedAbilitiesCard : HandDiscardDrawCard
    {
        public override string cardName => "Overestimated Abilities";
        public override string description => "You recall a time when you took on a task you thought you were capable of doing, only for it to be too gigantic for just one person to accomplish.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => -1;
    }

    public class TheCurseOfAVisionCard : ShuffleDeckCard
    {
        public override string cardName => "The Curse of a Vision";
        public override string description => "You recall a time when you attempted to achieve something you could only dream of, only to realize your dream was too big for the time and money you had available.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => -1;
    }

    public class AstronomicalRecalculationCard : FailureToSuccessCard
    {
        public override string cardName => "Astronomical Recalculation";
        public override string description => "You recall a time when you had to readjust a team project because things weren't working.";
        public override CardStat stat => CardStat.Integrity;
        public override int numberEffect => -1;
    }

    // Teamwork Cards
    public class AHelpingHand : GiveCard
    {
        public override string cardName => "A Helping Hand";
        public override string description => "You recall a time when you took on a supportive role for the group, assisting in getting the project done by uplifting others.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 2;
    }

    public class WeListenAndWeDontJudgeCard : ShareTopThreeCards
    {
        public override string cardName => "We Listen and We Don't Judge";
        public override string description => "You recall a time when you chose to listen to your teammates, and ended up finding thorough solutions because of it!";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 2;
    }

    public class TakingInitiativeCard : RecoverDiscardedCard
    {
        public override string cardName => "Taking Initiative";
        public override string description => "You recall a time when you stepped up as leader in a situation, working with teammates and making decisions that reflect the group as a whole.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 2;
        public override void SpecialEffect()
        {
            // Help a teammate recover a discarded card.
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class DungeonsAndDelegationsCard : NeutralCard
    {
        public override string cardName => "Dungeons and Delegations";
        public override string description => "You recall a time you played games with friends, and delegated tasks amongst one another.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 1;
    }

    public class TrustFallCard : NeutralCard
    {
        public override string cardName => "Trust Fall";
        public override string description => "You recall a time you built trust with your colleagues through a cultural exchange, learning from one another what they provide to the team.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 1;
    }

    public class MoodBoardCard : NeutralCard
    {
        public override string cardName => "Mood Board";
        public override string description => "You recall a time when you worked with friends to make a mood board, filled with one another's hopes, dreams, interests and the like, building a stronger bond amongst each other.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 1;
    }

    public class PartyRockerCard : NeutralCard
    {
        public override string cardName => "Party Rocker";
        public override string description => "You recall a time when you held a party, managing the event and making sure everyone was having a good time.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 1;
    }

    public class OpenNoteQuizCard : NeutralCard
    {
        public override string cardName => "Open Note Quiz";
        public override string description => "You recall a time when you shared notes with your team, helping each other better understand where to find solutions.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => 1;
    }

    public class BurntAndCrunchedCard : AskTeammateForCard
    {
        public override string cardName => "Burnt and Crunched";
        public override string description => "You recall a time when you pushed yourself to the limits to get things done, only for it to backfire and leave you unable to continue working at top strength.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => -1;
    }

    public class FightForTheCrownCard : PartialFailureToSuccessCard
    {
        public override string cardName => "Fight for the Crown";
        public override string description => "You recall a time when you fought for the leadership position, and as a result nothing was accomplished because of the frivolous arguments.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => -1;
    }

    public class TooManyEggsForOneBasketCard : HandDiscardDrawCard
    {
        public override string cardName => "Too Many Eggs for One Basket";
        public override string description => "You recall a time when you took on your teammates responsibilities instead of letting them do it, but because of that you ended up overworking yourself and leaving the team with nothing to contribute.";
        public override CardStat stat => CardStat.Teamwork;
        public override int numberEffect => -1;
    }

    // Communication Cards
    public class AllsWellThatEndsWellCard : RecoverDiscardedCard
    {
        public override string cardName => "All's Well that Ends Well";
        public override string description => "You recall a time when a discussion found a thorough solution, with all participants coming to a solid resolution.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 2;
    }

    public class FruitfulTruthsCard : DeckSearchCard
    {
        public override string cardName => "Fruitful Truths";
        public override string description => "You recall a time when you had to be clear and concise with your teammates, listing all positives and negatives, and it ended up helping you out greatly in the end.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 2;
    }

    public class TalkOfTheTownCard : GiveCard
    {
        public override string cardName => "Talk of the Town";
        public override string description => "You recall a time when you delivered a motivational pep talk, energizing your team and raising morale amongst all odds.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 2;
    }

    public class StalematesCard : NeutralCard
    {
        public override string cardName => "Stalemates";
        public override string description => "You recall a time when a conversation/argument fell on deaf ears, not reaching any sort of real resolution.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 1;
    }

    public class AdjustingConnectionsCard : NeutralCard
    {
        public override string cardName => "Adjusting Connections";
        public override string description => "You recall a time when you had to adjust your speaking style to better connect with your teammates.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 1;
    }

    public class AForEffortCard : NeutralCard
    {
        public override string cardName => "A for Effort";
        public override string description => "You recall a time when you provided crucial feedback to colleagues for their work.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 1;
    }

    public class ClearSummationCard : NeutralCard
    {
        public override string cardName => "Clear Summation";
        public override string description => "You recall a time when you summarized the goals of a project, outlining what needs to be accomplished and setting up a proper timeline.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 1;
    }

    public class HighStakesPitchCard : NeutralCard
    {
        public override string cardName => "High Stakes Pitch";
        public override string description => "You recall a time when you got out in front of a large audience to give a pitch/speech about an important topic to you.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => 1;
    }

    public class AllEarsNoMouthCard : FailureToSuccessCard
    {
        public override string cardName => "All Ears, No Mouth";
        public override string description => "You recall a time when a conversation was so one-sided, you weren't able to properly communicate.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => -1;
    }

    public class InterruptionsCard : HandDiscardDrawCard
    {
        public override string cardName => "Interruptions";
        public override string description => "You recall a time when you interrupted a colleague during a meeting, causing frustration.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => -1;
    }

    public class DetrimentalMisstepCard : ShuffleDeckCard
    {
        public override string cardName => "Detrimental Misstep";
        public override string description => "You recall a time you forgot to communicate key information to your team, which caused extreme frustration and arguments amongst the team.";
        public override CardStat stat => CardStat.Communication;
        public override int numberEffect => -1;
    }

}

namespace AbilityCards
{
    // Abstract base class for all Ability Cards
    public abstract class AbilityCard : CardBase
    {
        public override int numberEffect => 1;
    }

    // Awareness Ability Cards
    public class GroundedFocusCard : AbilityCard
    {
        public override string cardName => "Grounded Focus";
        public override string description => "Reduce your Stress by 1 for the round.";
        public override CardStat stat => CardStat.Awareness;
        public override void SpecialEffect()
        {
            // Reduce stress by 1 for the round
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class RefocusCard : AbilityCard
    {
        public override string cardName => "Refocus";
        public override string description => "Reroll your dice once this round.";
        public override CardStat stat => CardStat.Awareness;
        public override void SpecialEffect()
        {
            // Reroll dice once this round
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class MentalClarityCard : AbilityCard
    {
        public override string cardName => "Mental Clarity";
        public override string description => "Discard 1 bad memory and draw 1 good memory card.";
        public override CardStat stat => CardStat.Awareness;
        public override void SpecialEffect()
        {
            // Discard 1 bad memory and draw 1 good memory card
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class TacticalResetCard : AbilityCard
    {
        public override string cardName => "Tactical Reset";
        public override string description => "Discard your entire hand and draw 4 new cards.";
        public override CardStat stat => CardStat.Awareness;
        public override void SpecialEffect()
        {
            // Discard entire hand and draw 4 new cards
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    // Creativity Ability Cards
    public class CreativeSparkCard : AbilityCard
    {
        public override string cardName => "Creative Spark";
        public override string description => "Add +1 to a card if it is tied to Creativity.";
        public override CardStat stat => CardStat.Creativity;
        public override void SpecialEffect()
        {
            // Add +1 to a card if it is tied to Creativity
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class TriageCard : AbilityCard
    {
        public override string cardName => "Triage";
        public override string description => "Choose a player. They recover 1 chosen card from their discard pile.";
        public override CardStat stat => CardStat.Creativity;
        public override void SpecialEffect()
        {
            // Choose a player. They recover 1 chosen card from their discard pile
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class ImagineAWorldCard : AbilityCard
    {
        public override string cardName => "Imagine a World";
        public override string description => "If you have 3+ Stress, reduce it to 2 for this Scenario.";
        public override CardStat stat => CardStat.Creativity;
        public override void SpecialEffect()
        {
            // If you have 3+ Stress, reduce it to 2 for this Scenario
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class BufferCard : AbilityCard
    {
        public override string cardName => "Buffer";
        public override string description => "If the team fails this round, you do not discard a memory.";
        public override CardStat stat => CardStat.Creativity;
        public override void SpecialEffect()
        {
            // If the team fails this round, you do not discard a memory
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    // Integrity Ability Cards
    public class MomentumCard : AbilityCard
    {
        public override string cardName => "Momentum";
        public override string description => "Draw 2 cards, then discard 1.";
        public override CardStat stat => CardStat.Integrity;
        public override void SpecialEffect()
        {
            // Draw 2 cards, then discard 1
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class PauseAndReflectCard : AbilityCard
    {
        public override string cardName => "Pause and Reflect";
        public override string description => "Skip your turn this round to reduce your Stress by 2.";
        public override CardStat stat => CardStat.Integrity;
        public override void SpecialEffect()
        {
            // Skip your turn this round to reduce your Stress by 2
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class PreparationPaysCard : AbilityCard
    {
        public override string cardName => "Preparation Pays";
        public override string description => "Every player may play 1 additional card this round.";
        public override CardStat stat => CardStat.Integrity;
        public override void SpecialEffect()
        {
            // Every player may play 1 additional card this round
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class SteadyNervesCard : AbilityCard
    {
        public override string cardName => "Steady Nerves";
        public override string description => "Next round, the dice cannot result in a Failure (1–7 becomes 8).";
        public override CardStat stat => CardStat.Integrity;
        public override void SpecialEffect()
        {
            // Next round, the dice cannot result in a Failure (1–7 becomes 8)
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    // Teamwork Ability Cards
    public class ReorganizeResourcesCard : AbilityCard
    {
        public override string cardName => "Reorganize Resources";
        public override string description => "Everyone discards 1 card, then draws 1 card.";
        public override CardStat stat => CardStat.Teamwork;
        public override void SpecialEffect()
        {
            // Everyone discards 1 card, then draws 1 card
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class BoostMoraleCard : AbilityCard
    {
        public override string cardName => "Boost Morale";
        public override string description => "Add +1 to every teammate's next card played this round.";
        public override CardStat stat => CardStat.Teamwork;
        public override void SpecialEffect()
        {
            // Add +1 to every teammate's next card played this round
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class QuickAssessmentCard : AbilityCard
    {
        public override string cardName => "Quick Assessment";
        public override string description => "Look at the top 2 cards of any player's deck. Rearrange or discard one.";
        public override CardStat stat => CardStat.Teamwork;
        public override void SpecialEffect()
        {
            // Look at the top 2 cards of any player's deck. Rearrange or discard one
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class GroupCheckInCard : AbilityCard
    {
        public override string cardName => "Group Check-In";
        public override string description => "All players may draw 1 card OR discard a bad memory.";
        public override CardStat stat => CardStat.Teamwork;
        public override void SpecialEffect()
        {
            // All players may draw 1 card OR discard a bad memory
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    // Communication Ability Cards
    public class SynchronizeCard : AbilityCard
    {
        public override string cardName => "Synchronize";
        public override string description => "Choose two players. They may coordinate cards this round.";
        public override CardStat stat => CardStat.Communication;
        public override void SpecialEffect()
        {
            // Choose two players. They may coordinate cards this round
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class SwapSupportCard : AbilityCard
    {
        public override string cardName => "Swap Support";
        public override string description => "Make one teammate's negative card neutral.";
        public override CardStat stat => CardStat.Communication;
        public override void SpecialEffect()
        {
            // Make one teammate's negative card neutral
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class ExtendAHandCard : AbilityCard
    {
        public override string cardName => "Extend a Hand";
        public override string description => "Choose a teammate. They draw 1 card and reduce Stress by 1.";
        public override CardStat stat => CardStat.Communication;
        public override void SpecialEffect()
        {
            // Choose a teammate. They draw 1 card and reduce Stress by 1
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }

    public class CollectiveWisdomCard : AbilityCard
    {
        public override string cardName => "Collective Wisdom";
        public override string description => "Everyone may look at the top card of their inventory deck.";
        public override CardStat stat => CardStat.Communication;
        public override void SpecialEffect()
        {
            // Everyone may look at the top card of their inventory deck
            Debug.Log(cardName + " effect not yet implemented.");
        }
    }
}

namespace SupportCards
{
    // Awareness Support Cards
    public class DeepBreathCard : NeutralCard
    {
        public override string cardName => "Deep Breath";
        public override string description => "";
        public override CardStat stat => CardStat.Awareness;
    }

    public class ChallengingAssumptionCard : NeutralCard
    {
        public override string cardName => "Challenging Assumption";
        public override string description => "";
        public override CardStat stat => CardStat.Awareness;
    }

    public class ClearObservationCard : NeutralCard
    {
        public override string cardName => "Clear Observation";
        public override string description => "";
        public override CardStat stat => CardStat.Awareness;
    }

    public class EmpatheticEyesCard : NeutralCard
    {
        public override string cardName => "Empathetic Eyes";
        public override string description => "";
        public override CardStat stat => CardStat.Awareness;
    }

    public class WorldlyImpactCard : NeutralCard
    {
        public override string cardName => "Worldly Impact";
        public override string description => "";
        public override CardStat stat => CardStat.Awareness;
    }

    public class SocialWavesCard : NeutralCard
    {
        public override string cardName => "Social Waves";
        public override string description => "";
        public override CardStat stat => CardStat.Awareness;
    }

    // Creativity Support Cards
    public class SparklingRealizationCard : NeutralCard
    {
        public override string cardName => "Sparkling Realization";
        public override string description => "";
        public override CardStat stat => CardStat.Creativity;
    }

    public class InspirationOfLightCard : NeutralCard
    {
        public override string cardName => "Inspiration of Light";
        public override string description => "";
        public override CardStat stat => CardStat.Creativity;
    }

    public class ExpressiveConnectionCard : NeutralCard
    {
        public override string cardName => "Expressive Connection";
        public override string description => "";
        public override CardStat stat => CardStat.Creativity;
    }

    public class FuturePossibilitiesCard : NeutralCard
    {
        public override string cardName => "Future Possibilities";
        public override string description => "";
        public override CardStat stat => CardStat.Creativity;
    }

    public class ResourcefulPurposeCard : NeutralCard
    {
        public override string cardName => "Resourceful Purpose";
        public override string description => "";
        public override CardStat stat => CardStat.Creativity;
    }

    public class ReadyForTheUnknownCard : NeutralCard
    {
        public override string cardName => "Ready for the Unknown";
        public override string description => "";
        public override CardStat stat => CardStat.Creativity;
    }

    // Integrity Support Cards
    public class ValuableActionCard : NeutralCard
    {
        public override string cardName => "Valuable Action";
        public override string description => "";
        public override CardStat stat => CardStat.Integrity;
    }

    public class HardTruthCard : NeutralCard
    {
        public override string cardName => "Hard Truth";
        public override string description => "";
        public override CardStat stat => CardStat.Integrity;
    }

    public class MistakesWillPassCard : NeutralCard
    {
        public override string cardName => "Mistakes will Pass";
        public override string description => "";
        public override CardStat stat => CardStat.Integrity;
    }

    public class ProbableThinkingCard : NeutralCard
    {
        public override string cardName => "Probable Thinking";
        public override string description => "";
        public override CardStat stat => CardStat.Integrity;
    }

    public class WhoCallsTheNormCard : NeutralCard
    {
        public override string cardName => "Who Calls the Norm?";
        public override string description => "";
        public override CardStat stat => CardStat.Integrity;
    }

    public class HonorFocusCard : NeutralCard
    {
        public override string cardName => "Honor Focus";
        public override string description => "";
        public override CardStat stat => CardStat.Integrity;
    }

    // Teamwork Support Cards
    public class SharedTrustCard : NeutralCard
    {
        public override string cardName => "Shared Trust";
        public override string description => "";
        public override CardStat stat => CardStat.Teamwork;
    }

    public class AdaptNRelyCard : NeutralCard
    {
        public override string cardName => "Adapt n' Rely";
        public override string description => "";
        public override CardStat stat => CardStat.Teamwork;
    }

    public class CelebrationUnitesCard : NeutralCard
    {
        public override string cardName => "Celebration Unites";
        public override string description => "";
        public override CardStat stat => CardStat.Teamwork;
    }

    public class ValuedInputCard : NeutralCard
    {
        public override string cardName => "Valued Input";
        public override string description => "";
        public override CardStat stat => CardStat.Teamwork;
    }

    public class HarmonizeLightCard : NeutralCard
    {
        public override string cardName => "Harmonize Light";
        public override string description => "";
        public override CardStat stat => CardStat.Teamwork;
    }

    public class ShoulderOfSupportCard : NeutralCard
    {
        public override string cardName => "Shoulder of Support";
        public override string description => "";
        public override CardStat stat => CardStat.Teamwork;
    }

    // Communication Support Cards
    public class PurposeOfWordsCard : NeutralCard
    {
        public override string cardName => "Purpose of Words";
        public override string description => "";
        public override CardStat stat => CardStat.Communication;
    }

    public class ActiveListeningCard : NeutralCard
    {
        public override string cardName => "Active Listening";
        public override string description => "";
        public override CardStat stat => CardStat.Communication;
    }

    public class IntentionalClarityCard : NeutralCard
    {
        public override string cardName => "Intentional Clarity";
        public override string description => "";
        public override CardStat stat => CardStat.Communication;
    }

    public class VulnerableRootsCard : NeutralCard
    {
        public override string cardName => "Vulnerable Roots";
        public override string description => "";
        public override CardStat stat => CardStat.Communication;
    }

    public class TalesFlutterCard : NeutralCard
    {
        public override string cardName => "Tales Flutter";
        public override string description => "";
        public override CardStat stat => CardStat.Communication;
    }

    public class AmplifyEndsCard : NeutralCard
    {
        public override string cardName => "Amplify Ends";
        public override string description => "";
        public override CardStat stat => CardStat.Communication;
    }
}
