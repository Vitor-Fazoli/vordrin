"use client";

import PlayerTab from '@/components/player-tab';
import ProgressBar from '@/components/progress-bar';
import { useState, useEffect } from 'react';


export default function GamePage() {
  function gameLoop() {

  }

  setInterval(gameLoop, 1000 / 60);


  return (
    <div className='w-full h-full'>
      <div className="flex w-full ">
        <aside className='bg-red-400 w-1/12 h-full'>

        </aside>
        <main className='w-11/12 h-full flex flex-col gap-5 justify-center items-center'>
          <div className='w-1/2 pt-4'>
            <ProgressBar label='TESTE' progress={20} maxValue={100}></ProgressBar >
          </div>
          <PlayerTab></PlayerTab>
        </main>
      </div>
      <div className="absolute bottom-0 w-full">
        <ProgressBar progress={0} maxValue={100} small={true}></ProgressBar>
      </div>
    </div>
  );
}
