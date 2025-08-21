"use client";

import { useState } from "react";
import { Suit } from "./models/Suit";
import { CardValue } from "./models/CardValue";
import { Face } from "./models/Face";
import { randomInt } from "crypto";
import Card from "./card";

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

function Deck() {
  const [cards, setCards] = useState<CardValue[]>(GetFullDeck());

  function DrawTopCard() {
    return DrawCard(0);
  }
  function DrawRandomCard() {
    const position = randomInt(cards.length);
    return DrawCard(position);
  }
  function DrawCard(position: number) {
    const drawn = cards.splice(position, 1)[0];

    setCards(cards);
    return drawn;
  }

  const testDisplayAllCards = cards.map((c) => (
    <Card suit={c.suit} face={c.face} key={c.suit + c.face} />
  ));

  return <div>{testDisplayAllCards}</div>;
}
export default Deck;
