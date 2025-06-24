using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    public abstract string cardName { get; }
    public abstract int numberEffect { get; }
    public abstract void SpecialEffect();
    // need to add description property
}

public abstract class GiveCard : CardBase
{
    public override void SpecialEffect()
    {
        // Give card method
        throw new System.NotImplementedException();
    }
}

public class AHelpingHand : GiveCard
{
    public override string cardName => "A Helping Hand";
    public override int numberEffect => 2;
}

public class EurekaCard : CardBase
{
    public override string cardName => "EUREKA!";
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
    public override int numberEffect => 2;
    public override void SpecialEffect()
    {
        // After playing this card, draw 2 cards.
        throw new System.NotImplementedException();
    }
}

public class AdaptTheWorldCard : GiveCard
{
    public override string cardName => "Adapt the World";
    public override int numberEffect => 2;
}

public class ArtTherapyCard : CardBase
{
    public override string cardName => "Art Therapy";
    public override int numberEffect => 1;
    public override void SpecialEffect()
    {
        // No effect
        return;
    }
}