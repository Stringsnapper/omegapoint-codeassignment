import type { PlayerProps } from "../interfaces/Player";
import PlayerCard from "./PlayerCard";

interface PlayerListProps {
	players: PlayerProps[];
	deleteAction?: (playerId: string) => Promise<boolean>;
	giveXpAction?: (playerProps: PlayerProps, amount: number) => Promise<boolean>;
}

const PlayerList: React.FC<PlayerListProps> = (props) => {
	const playerCards = props.players.map((player, index) => {
		return (
			<PlayerCard key={index} playerProps={player} deleteAction={props.deleteAction} giveXpAction={props.giveXpAction}/>
		);
	});
	
	return(
		<div>
			{playerCards}
		</div>
	);
}

export default PlayerList;