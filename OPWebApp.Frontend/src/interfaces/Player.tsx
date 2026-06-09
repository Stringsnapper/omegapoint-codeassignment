import React from "react";

export interface PlayerProps {

	id: string;
	name: string;
	level: number;
	xp: number;
	description: string;
}

declare const Player: React.FC<PlayerProps>;

export default Player;