import React, { useState, useEffect } from 'react';
import joystick from './assets/joystick.png'

import './App.css'
import type { PlayerProps } from './modules/Player';



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
