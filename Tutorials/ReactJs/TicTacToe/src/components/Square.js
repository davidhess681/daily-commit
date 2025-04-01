export default function Square({ value, onSquareClicked }) {

    return <button className="square" onClick={onSquareClicked}>{value}</button>;
}