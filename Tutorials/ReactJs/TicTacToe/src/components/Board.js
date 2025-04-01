import Square from "./Square";

export default function Board({ xIsNext, squares, onPlay }) {
    function handleClick(i) {
        if (squares[i] || calculateWinner(squares)) {
            return;
        }
        const nextSquares = squares.slice();
        if (xIsNext) {
            nextSquares[i] = "X";
        }
        else {
            nextSquares[i] = "O";
        }
        onPlay(nextSquares);
    }

    const winner = calculateWinner(squares);
    let status;
    if (winner) {
        status = "Winner: " + winner;
    }
    else {
        status = "Next player: " + (xIsNext ? "X" : "O")
    }


    function row(indexes) {
        return indexes.map(index =>
            <Square value={squares[index]} onSquareClicked={() => handleClick(index)} />
        );
    }

    const rows = [[0, 1, 2], [3, 4, 5], [6, 7, 8]].map(i =>
        <div className="board-row">
            {row(i)}
        </div>
    )

    return (
        <>
            <div className="status">{status}</div>
            {rows}
        </>
    )
}

function calculateWinner(squares) {
    const lines = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];
    for (let i = 0; i < lines.length; i++) {
        const [a, b, c] = lines[i];
        if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
            return squares[a];
        }
    }
    return null;
}