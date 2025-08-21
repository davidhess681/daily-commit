import { Face } from "./models/Face";
import { Suit } from "./models/Suit";

interface CardProps {
  suit: Suit;
  face: Face;
}
function Card({ suit, face }: CardProps) {
  return (
    <div>
      {face} of {suit}
    </div>
  );
}

export default Card;
