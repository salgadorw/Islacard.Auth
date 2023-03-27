import { useState } from 'react'
import { PersonalInfo } from './components/PersonalInfo/PersonalInfo'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <div className="App">
      <PersonalInfo></PersonalInfo>
    </div>
  )
}

export default App
