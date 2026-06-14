import { useEffect, useState } from "react";
import type { PlayerProps } from "../interfaces/Player";
import PlayerCard from "./PlayerCard";

interface PlayerListProps {
	players: PlayerProps[];
	deleteAction?: (playerId: string) => Promise<boolean>;
	giveXpAction?: (playerProps: PlayerProps, amount: number) => Promise<boolean>;
	setCurrentPlayerAction?: (playerProps: PlayerProps) => void;
	currentPlayer?: PlayerProps;
}

const PlayerList: React.FC<PlayerListProps> = (props) => {

	const [selectedIndex, setSelectedIndex] = useState<number>(-1)

	useEffect(() => {
		if (props.currentPlayer) {
			const index = props.players.findIndex(player => player.id === props.currentPlayer.id);
			setSelectedIndex(index);
		} else {
			setSelectedIndex(-1);
		}
	}, [props.currentPlayer])

	const handleCardClick = (key: number, playerProps: PlayerProps) => {
		console.log("Clicked card with index: " + key);
		if (selectedIndex !== key) {
			setSelectedIndex(key);
			props.setCurrentPlayerAction?.(playerProps);
		}
	}

	const playerCards = props.players.map((player, index) => {
		return (
			<PlayerCard key={index} index={index} playerProps={player} deleteAction={props.deleteAction} giveXpAction={props.giveXpAction} selectedCardIndex={selectedIndex} onClickAction={handleCardClick}/>
		);
	});
	
	return(
		<div>
			{playerCards}
		</div>
	);
}

export default PlayerList;