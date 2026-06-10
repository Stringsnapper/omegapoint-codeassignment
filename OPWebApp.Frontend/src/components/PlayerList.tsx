import type { PlayerProps } from "../interfaces/Player";
import PlayerCard from "./PlayerCard";

interface PlayerListProps {
	players: PlayerProps[];
}

const PlayerList: React.FC<PlayerListProps> = (props) => {
	const playerCards = props.players.map((player) => {
		return<PlayerCard playerProps={player}/>
	});
	
	return(
		<div>
			{playerCards}
		</div>
	);
}

export default PlayerList;