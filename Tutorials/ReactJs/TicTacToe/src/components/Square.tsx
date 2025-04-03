interface SquareProps {
  value: string;
  onSquareClicked: () => void;
}

function Square({ value, onSquareClicked }: SquareProps) {
  return (
    <button className="square" onClick={onSquareClicked}>
      {value}
    </button>
  );
}

export default Square;
