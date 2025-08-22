"use client";

import { useState } from "react";
import { Suit } from "./models/Suit";
import { CardValue } from "./models/CardValue";
import { Face } from "./models/Face";
import Card from "./card";

interface DeckProps {
  onCardDrawn: (card: CardValue) => void;
}

function GetFullDeck(): CardValue[] {
  const deck: CardValue[] = [];
  Object.values(Suit).forEach((suit) => {
    Object.values(Face).forEach((face) => {
      const card: CardValue = {
        suit: suit as Suit,
        face: face as Face,
      };
      deck.push(card);
    });
  });
  return deck;
}

function Deck({ onCardDrawn }: DeckProps) {
  const [cards, setCards] = useState<CardValue[]>(GetFullDeck());

  function DrawTopCard() {
    DrawCard(0);
  }
  function DrawRandomCard() {
    const position = Math.floor(Math.random() * cards.length);
    DrawCard(position);
  }
  function DrawCard(position: number) {
    const drawn = cards.splice(position, 1)[0];

    setCards(cards);
    onCardDrawn(drawn);
  }

  const testDisplayAllCards = cards.map((c) => (
    <Card suit={c.suit} face={c.face} key={c.suit + c.face} />
  ));

  return (
    <div>
      <button onClick={DrawTopCard}>Draw top card</button>
      <button onClick={DrawRandomCard}>Draw random card</button>
      <div>
        All the cards:
        {testDisplayAllCards}
      </div>
    </div>
  );
}
export default Deck;
