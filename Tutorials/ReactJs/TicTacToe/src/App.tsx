import Game from "./components/Game";

function App() {
  return (
    <>
      <h1>I am a level one heading</h1>

      <p>
        This is a paragraph of text. In the text is a<span>span element</span>{" "}
        and also a<a href="https://example.com">link</a>.
      </p>

      <p>
        This is the second paragraph. It contains an <em>emphasized</em>{" "}
        element.
      </p>

      <ul>
        <li>
          Item <span>one</span>
        </li>
        <li className="special">Item two</li>
        <li>
          Item <em>three</em>
        </li>
      </ul>
      <em>Not purple</em>
      <div className="outer">
        <div className="box">The inner box is 90% - 30px.</div>
      </div>

      <p>Here is my game</p>
      <Game />
    </>
  );
}

export default App;
