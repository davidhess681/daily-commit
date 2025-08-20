import { Face } from "./models/Face";
import { Suit } from "./models/Suit";

interface CardProps {
  suit: Suit;
  face: Face;
}
function Card({ suit, face }: CardProps) {
  return (
    <div>
      <div>{face}</div>
      <div>{suit}</div>
    </div>
  );
}

export default Card;
