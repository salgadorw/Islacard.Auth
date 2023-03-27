import { useState } from 'react';
import reactLogo from './assets/react.svg';
import { Button } from 'react-bootstrap';
import { Login } from './components/Login/Login';
function App() {
  const [count, setCount] = useState(0);

  return (
    <div className="col-md-6 offset-md-3 mt-5">
      <h3>Isracard Login</h3>
      <Login />
    </div>
  );
}

export default App;