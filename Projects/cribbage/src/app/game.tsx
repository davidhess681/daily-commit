"use client";

import { useState } from "react";
import Deck from "./deck";
import { CardValue } from "./models/CardValue";
import Card from "./card";

function Game() {
  const [displayedCard, setDisplayedCard] = useState<CardValue>();
  function drawCard(card: CardValue) {
    setDisplayedCard(card);
  }
  return (
    <>
      <div>
        {displayedCard && (
          <>
            Current Card:
            <Card suit={displayedCard?.suit} face={displayedCard?.face} />
          </>
        )}
      </div>
      <Deck onCardDrawn={drawCard}></Deck>
    </>
  );
}
export default Game;
