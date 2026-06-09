import React, { useState, useEffect } from 'react';
import joystick from './assets/joystick.png'

import './App.css'
import type { PlayerProps } from './interfaces/Player';
import PlayerForm from './components/PlayerForm';



function App() {
  const [items, setItems] = useState<PlayerProps[]>([]);
  const [refresh, setRefresh] = useState(false);

  useEffect(() => {
    console.log("Fetching players...");
    fetch("http://localhost:5267/api/players", { method: "GET" })
      .then(response => {
        return response.json();
      })
      .then(data => {
        setItems(data);
      })
  }, [refresh]);

  const handleSubmit = async (playerProps: PlayerProps) : Promise<boolean> => {
    console.debug("Submitting player:", playerProps);
    var success = false;
    await fetch("http://localhost:5267/api/players", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(playerProps)
    })
    .then(response => {
      if (response.ok) {
        console.debug("Player created successfully");
        setRefresh(!refresh);
        success = true;
      }
      else {
        console.error("Failed to create player:", response.statusText);
        success = false;
      }
    })
    return success;
    
  }

  var playerProps : PlayerProps = {
    id: '',
    name: '',
    level: 1,
    xp: 0,
    description: '',
  }
  return (
    <>
      <section id="center">
        <div className="hero">
          <img src={joystick} className="base" alt="joystick logo"/>
          
        </div>
        <div>
          <h1>Players</h1>
          <p>
            Manage your players here.
          </p>
          <ul>
            {items.map((item, index) => (
              <li key={index}>{item.name} - {item.level}</li>
            ))}
          </ul>
        </div>
        <div>
          <PlayerForm submitAction={handleSubmit} playerProps={playerProps}/>
        </div>
        
      </section>

      <div className="ticks"></div>

      <section id="next-steps">
        <div id="docs">
          <svg className="icon" role="presentation" aria-hidden="true">
            <use href="/icons.svg#documentation-icon"></use>
          </svg>
        </div>
        <div id="social">
          <svg className="icon" role="presentation" aria-hidden="true">
            <use href="/icons.svg#social-icon"></use>
          </svg>
          
        </div>
      </section>

      <div className="ticks"></div>
      <section id="spacer">
          <h3>Credits</h3>
          <a href="https://www.flaticon.com/free-icons/video-game-controller" title="video game controller icons">Video game controller icons created by Hilmy Abiyyu A. - Flaticon</a>
      </section>
    </>
  );
};

export default App
