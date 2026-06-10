import type { PlayerProps } from "../interfaces/Player";

interface PlayerCardProps {
	playerProps: PlayerProps;
}

const PlayerCard: React.FC<PlayerCardProps> = props => {
	var playerProps = props.playerProps;
	return(
		<div>
			<h2>{playerProps.name}</h2>
			<p>Level: {playerProps.level}</p>
			<p>XP: {playerProps.xp}</p>
			<p>{playerProps.description}</p>
		</div>
	);
}
export default PlayerCard;