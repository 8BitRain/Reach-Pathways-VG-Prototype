using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    public abstract string cardName { get; }
    public abstract string description { get; }
    public abstract int numberEffect { get; }
    public abstract void SpecialEffect();
}

// Methods for shared effects
public abstract class GiveCard : CardBase
{
    public override void SpecialEffect()
    {
        // Give card method
        throw new System.NotImplementedException();
    }
}

// Innovator Cards
public class EurekaCard : CardBase
{
    public override string cardName => "EUREKA!";
    public override string description => "You recall a time you successfully worked through a problem with a clever solution.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Look through the deck, and choose any one card before reshuffling. 
        throw new System.NotImplementedException();
    }
}

public class UnconventionalHackCard : CardBase
{
    public override string cardName => "Unconventional Hack";
    public override string description => "You recall a time your unconventional solution saved Kaharaba's main building.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Draw 2 cards after playing.
        throw new System.NotImplementedException();
    }
}

public class AdaptTheWorldCard : GiveCard
{
    public override string cardName => "Adapt the World";
    public override string description => "You recall a time you reworked an existing solution to fix an age old problem.";
    public override int numberEffect => 2;
}

public class ArtTherapyCard : CardBase
{
    public override string cardName => "Art Therapy";
    public override string description => "You recall a time you used a creative medium to revitalize your team.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class WeatherTheBrainstormCard : CardBase
{
    public override string cardName => "Weather the Brainstorm";
    public override string description => "You recall a time your brainstorm produced a number of ideas, but you couldn't settle on one.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class BlueElectricityWhiteSmokeCard : CardBase
{
    public override string cardName => "Blue Electricity, White Smoke";
    public override string description => "You recall a time your invention worked before fizzling out in a dramatic display of smoke.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class MissingEyebrowsCard : CardBase
{
    public override string cardName => "Missing Eyebrows";
    public override string description => "You recall a time your invention backfired and hurt you badly.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Partial failure becomes a success.
        throw new System.NotImplementedException();
    }
}

public class StuckInARutCard : CardBase
{
    public override string cardName => "Stuck In A Rut";
    public override string description => "You recall a time you could not shake the brain fog. The solution remained just beyond the haze.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Discard as many cards in your hand as you like, then draw that many cards.
        throw new System.NotImplementedException();
    }
}

public class WatchItBurnCard : CardBase
{
    public override string cardName => "Watch It Burn";
    public override string description => "You recall a time the idea you created caused exhaustive arguments between the guild members.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Ask a teammate for a card from their hand.
        throw new System.NotImplementedException();
    }
}

// Strategist Cards
public class TimedJustRightCard : CardBase
{
    public override string cardName => "Timed Just Right";
    public override string description => "You recall a time you crafted a work schedule for your team which helped the guild run smoothly.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Look through the deck, and choose any one card before reshuffling
        throw new System.NotImplementedException();
    }
}

public class AllInOnePieceCard : GiveCard
{
    public override string cardName => "All in One Piece";
    public override string description => "You recall a time the pieces of your plan all came together, and it went perfectly.";
    public override int numberEffect => 2;
}

public class BreatheInBreatheOutCard : CardBase
{
    public override string cardName => "Breathe In, Breathe Out";
    public override string description => "You recall a time you led your team in wellness exercises to focus them, brighten their spirits, and ease their stress.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Look at the top three cards in the deck and share with your teammates
        throw new System.NotImplementedException();
    }
}

public class PerceivedRisksCard : CardBase
{
    public override string cardName => "Perceived Risks";
    public override string description => "You recall a time you noticed the potentially dangerous risks in a prospective plan.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class RestAndRecuperateCard : CardBase
{
    public override string cardName => "Rest and Recuperate";
    public override string description => "You recall a time you perceived the stress in your body before an important meeting and rested in Afya.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class FriendInNeedCard : CardBase
{
    public override string cardName => "Friend in Need";
    public override string description => "You recall a time a guild member accompanied you on a nature walk after noticing you ignoring your own stress.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class EmotionalOutburstCard : CardBase
{
    public override string cardName => "Emotional Outburst";
    public override string description => "You recall a time the frustration got to you, and you shouted at your team.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Shuffle the deck
        throw new System.NotImplementedException();
    }
}

public class TearToPiecesCard : CardBase
{
    public override string cardName => "Tear to Pieces";
    public override string description => "You recall a time you did not notice the escalating tension in the guild, and stood by as it boiled over into an argument.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Recover any discarded card.
        throw new System.NotImplementedException();
    }
}

public class LackOfAwarenessCard : CardBase
{
    public override string cardName => "Lack of Awareness";
    public override string description => "You recall a time you overlooked the growing stress in the guild and morale took a massive hit.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Partial failure becomes a success.
        throw new System.NotImplementedException();
    }
}

// Visionary Cards
public class APinchOfPunctualityCard : GiveCard
{
    public override string cardName => "A Pinch of Punctuality";
    public override string description => "You recall a time when you arrived on time for a meeting, ready to take on the world.";
    public override int numberEffect => 2;
}

public class TheGiftOfAVisionCard : CardBase
{
    public override string cardName => "The Gift of a Vision";
    public override string description => "You recall a time when you saw your dreams through, and it came out exactly as you hoped it could have.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Look through the deck, and choose any one card before reshuffling.
        throw new System.NotImplementedException();
    }
}

public class PromisesPromisesCard : CardBase
{
    public override string cardName => "Promises, Promises";
    public override string description => "You recall a time when you followed through with a commitment, securing the trust of your team.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Recover a random card from the discard pile.
        throw new System.NotImplementedException();
    }
}

public class ElephantInTheRoomCard : CardBase
{
    public override string cardName => "Elephant in the Room";
    public override string description => "You recall a time when you needed to address unethical behavior of a colleague in a constructive manner.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class AdvocationPracticesCard : CardBase
{
    public override string cardName => "Advocation Practices";
    public override string description => "You recall a time you spoke up about utilizing ethical practices during a high-stakes project.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class DelayedAccountabilityCard : CardBase
{
    public override string cardName => "Delayed Accountability";
    public override string description => "You recall a time when your lack of accountability led to a major project delay.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class TheBiggerPictureCard : CardBase
{
    public override string cardName => "The Bigger Picture";
    public override string description => "You recall a time when you were trying to create something but couldn't wrap your head around it, until stepping away to unveil crucial revelations.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class SuperSonicPotentialCard : CardBase
{
    public override string cardName => "Super-Sonic Potential";
    public override string description => "You recall a time when you brought an idea to the table, and gathered an extensive team to take it on.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class OverestimatedAbilitiesCard : CardBase
{
    public override string cardName => "Overestimated Abilities";
    public override string description => "You recall a time when you took on a task you thought you were capable of doing, only for it to be too gigantic for just one person to accomplish.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Discard as many cards in your hand as you like, then draw that many cards.
        throw new System.NotImplementedException();
    }
}

public class TheCurseOfAVisionCard : CardBase
{
    public override string cardName => "The Curse of a Vision";
    public override string description => "You recall a time when you attempted to achieve something you could only dream of, only to realize your dream was too big for the time and money you had available.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Shuffle the deck.
        throw new System.NotImplementedException();
    }
}

public class AstronomicalRecalculationCard : CardBase
{
    public override string cardName => "Astronomical Recalculation";
    public override string description => "You recall a time when you had to readjust a team project because things weren't working.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Failed roll becomes a success.
        throw new System.NotImplementedException();
    }
}

// Collaborator Cards
public class AHelpingHand : GiveCard
{
    public override string cardName => "A Helping Hand";
    public override string description => "You recall a time when you took on a supportive role for the group, assisting in getting the project done by uplifting others.";
    public override int numberEffect => 2;
}

public class WeListenAndWeDontJudgeCard : CardBase
{
    public override string cardName => "We Listen and We Don't Judge";
    public override string description => "You recall a time when you chose to listen to your teammates, and ended up finding thorough solutions because of it!";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Look at the first 3 cards at the top of the deck and share with your teammates
        throw new System.NotImplementedException();
    }
}

public class TakingInitiativeCard : CardBase
{
    public override string cardName => "Taking Initiative";
    public override string description => "You recall a time when you stepped up as leader in a situation, working with teammates and making decisions that reflect the group as a whole.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Help a teammate recover a discarded card.
        throw new System.NotImplementedException();
    }
}

public class DungeonsAndDelegationsCard : CardBase
{
    public override string cardName => "Dungeons and Delegations";
    public override string description => "You recall a time you played games with friends, and delegated tasks amongst one another.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class TrustFallCard : CardBase
{
    public override string cardName => "Trust Fall";
    public override string description => "You recall a time you built trust with your colleagues through a cultural exchange, learning from one another what they provide to the team.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class MoodBoardCard : CardBase
{
    public override string cardName => "Mood Board";
    public override string description => "You recall a time when you worked with friends to make a mood board, filled with one another's hopes, dreams, interests and the like, building a stronger bond amongst each other.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class PartyRockerCard : CardBase
{
    public override string cardName => "Party Rocker";
    public override string description => "You recall a time when you held a party, managing the event and making sure everyone was having a good time.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class OpenNoteQuizCard : CardBase
{
    public override string cardName => "Open Note Quiz";
    public override string description => "You recall a time when you shared notes with your team, helping each other better understand where to find solutions.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class BurntAndCrunchedCard : CardBase
{
    public override string cardName => "Burnt and Crunched";
    public override string description => "You recall a time when you pushed yourself to the limits to get things done, only for it to backfire and leave you unable to continue working at top strength.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Ask a teammate for a card from their hand.
        throw new System.NotImplementedException();
    }
}

public class FightForTheCrownCard : CardBase
{
    public override string cardName => "Fight for the Crown";
    public override string description => "You recall a time when you fought for the leadership position, and as a result nothing was accomplished because of the frivolous arguments.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Partial failure becomes a success.
        throw new System.NotImplementedException();
    }
}

public class TooManyEggsForOneBasketCard : CardBase
{
    public override string cardName => "Too Many Eggs for One Basket";
    public override string description => "You recall a time when you took on your teammates responsibilities instead of letting them do it, but because of that you ended up overworking yourself and leaving the team with nothing to contribute.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Discard as many cards in your hand as you like, then draw that many cards.
        throw new System.NotImplementedException();
    }
}

// Communicator Cards
public class AllsWellThatEndsWellCard : CardBase
{
    public override string cardName => "All's Well that Ends Well";
    public override string description => "You recall a time when a discussion found a thorough solution, with all participants coming to a solid resolution.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Recover any card that has been discarded.
        throw new System.NotImplementedException();
    }
}

public class FruitfulTruthsCard : CardBase
{
    public override string cardName => "Fruitful Truths";
    public override string description => "You recall a time when you had to be clear and concise with your teammates, listing all positives and negatives, and it ended up helping you out greatly in the end.";
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // Look through the deck, and choose any one card before reshuffling.
        throw new System.NotImplementedException();
    }
}

public class TalkOfTheTownCard : GiveCard
{
    public override string cardName => "Talk of the Town";
    public override string description => "You recall a time when you delivered a motivational pep talk, energizing your team and raising morale amongst all odds.";
    public override int numberEffect => 2;
}

public class StalematesCard : CardBase
{
    public override string cardName => "Stalemates";
    public override string description => "You recall a time when a conversation/argument fell on deaf ears, not reaching any sort of real resolution.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class AdjustingConnectionsCard : CardBase
{
    public override string cardName => "Adjusting Connections";
    public override string description => "You recall a time when you had to adjust your speaking style to better connect with your teammates.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class AForEffortCard : CardBase
{
    public override string cardName => "A for Effort";
    public override string description => "You recall a time when you provided crucial feedback to colleagues for their work.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class ClearSummationCard : CardBase
{
    public override string cardName => "Clear Summation";
    public override string description => "You recall a time when you summarized the goals of a project, outlining what needs to be accomplished and setting up a proper timeline.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class HighStakesPitchCard : CardBase
{
    public override string cardName => "High Stakes Pitch";
    public override string description => "You recall a time when you got out in front of a large audience to give a pitch/speech about an important topic to you.";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}

public class AllEarsNoMouthCard : CardBase
{
    public override string cardName => "All Ears, No Mouth";
    public override string description => "You recall a time when a conversation was so one-sided, you weren't able to properly communicate.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Failed roll becomes a success.
        throw new System.NotImplementedException();
    }
}

public class InterruptionsCard : CardBase
{
    public override string cardName => "Interruptions";
    public override string description => "You recall a time when you interrupted a colleague during a meeting, causing frustration.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Discard as many cards in your hand as you like, then draw that many cards.
        throw new System.NotImplementedException();
    }
}

public class DetrimentalMisstepCard : CardBase
{
    public override string cardName => "Detrimental Misstep";
    public override string description => "You recall a time you forgot to communicate key information to your team, which caused extreme frustration and arguments amongst the team.";
    public override int numberEffect => -1;
    public override void SpecialEffect()
    {
        // Shuffle the deck
        throw new System.NotImplementedException();
    }
}
