interface SquareProps {
  value: string;
  onSquareClicked: () => void;
}

export default function Square({ value, onSquareClicked }: SquareProps) {
  return (
    <button className="square" onClick={onSquareClicked}>
      {value}
    </button>
  );
}
