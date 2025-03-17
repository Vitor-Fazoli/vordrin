import { useState, useEffect } from 'react';
import ProgressBar from "./progress-bar";

interface PlayerStats {
    health: number;
    mana: number;
    attack: number;
    defense: number;
    level: number;
    experience: number;
    dodgeChance: number;
    dodgeCooldown: number;
}

const PlayerTab = () => {
    const [isOnCooldown, setIsOnCooldown] = useState(false);
    const [cooldownTimer, setCooldownTimer] = useState(0);
    const dodgeCooldownTime = 5000; // 5 seconds cooldown

    const handleDodge = () => {
        if (!isOnCooldown) {
            setIsOnCooldown(true);
            setCooldownTimer(dodgeCooldownTime);

            // Add dodge logic here
            const dodgeChance = 0.3; // 30% chance to dodge
            const isDodgeSuccessful = Math.random() < dodgeChance;

            if (isDodgeSuccessful) {
                console.log('Dodge successful!');
                // Add dodge success effects here
            }
        }
    };

    useEffect(() => {
        let interval: NodeJS.Timeout;

        if (isOnCooldown && cooldownTimer > 0) {
            interval = setInterval(() => {
                setCooldownTimer(prev => {
                    if (prev <= 0) {
                        setIsOnCooldown(false);
                        clearInterval(interval);
                        return 0;
                    }
                    return prev - 100;
                });
            }, 100);
        }

        return () => {
            if (interval) clearInterval(interval);
        };
    }, [isOnCooldown, cooldownTimer]);

    return (
        <div className="border border-rose-900/50 p-2">
            <h2><span className="text-rose-700 outline mr-0.5 font-bold">O</span>ilmann</h2>
            <div className="flex gap-10">
                <div>
                    <p>Health</p>
                    <ProgressBar progress={100} maxValue={100} small={true}></ProgressBar>
                    <p>Shield</p>
                    <ProgressBar progress={100} maxValue={100} small={true}></ProgressBar>
                </div>
                <div
                    className="relative size-10 cursor-pointer"
                    onClick={handleDodge}
                    title={isOnCooldown ? `Dodge cooldown: ${(cooldownTimer / 1000).toFixed(1)}s` : 'Click to dodge'}
                >
                    <div className={`absolute inset-0 border border-rose-900 rounded-full ${isOnCooldown ? 'animate-ping' : ''} opacity-75`}></div>
                    <div className="relative border border-rose-900 rounded-full size-10"></div>
                </div>
            </div>
        </div>
    );
};

export default PlayerTab;
