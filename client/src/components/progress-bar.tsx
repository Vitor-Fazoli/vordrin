interface ProgressBarProps {
    progress: number;
    maxValue: number;
    label?: string;
    small?: boolean;
}

const ProgressBar = ({ progress, maxValue, label, small }: ProgressBarProps) => {
    const percentage = Math.min((progress / maxValue) * 100, 100);
    if (small) {
        return (
            <div className="w-full">
                <div className="h-1 w-full bg-rose-900/50 backdrop-blur-sm">
                    <div
                        className="h-full bg-rose-700 transition-all duration-300 ease-in-out"
                        style={{ width: `${percentage}%` }}
                    >
                    </div>
                </div>
            </div>
        );
    } else {
        return (
            <div className="w-full">
                <div className="h-4 w-full bg-rose-900/50 backdrop-blur-sm outline outline-rose-900/50 outline-offset-2">
                    <div
                        className="h-full bg-rose-700 transition-all duration-300 ease-in-out"
                        style={{ width: `${percentage}%` }}
                    >
                        <div className="absolute inset-0 flex items-center justify-center">
                            <span className="text-sm font-bold text-rose-100">
                                {label}
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};

export default ProgressBar;
