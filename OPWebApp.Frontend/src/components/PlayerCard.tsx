import { useEffect, useState, type CSSProperties } from "react";
import type { PlayerProps } from "../interfaces/Player";

interface PlayerCardProps {
	index: number;
	playerProps: PlayerProps;
	selectedCardIndex: number;
	deleteAction?: (playerId: string) => Promise<boolean>;
	giveXpAction?: (playerProps: PlayerProps, amount: number) => Promise<boolean>;
	onClickAction?: (index: number, playerProps: PlayerProps) => void;
}




const PlayerCard: React.FC<PlayerCardProps> = props => {

	const [hover, setHover] = useState(false);
	const [selected, setSelected] = useState(false);

	useEffect(() => {
		if (props.selectedCardIndex !== props.index) {
			setSelected(false);
		}
	}, [props.selectedCardIndex])

	function determineBorder () {
		if(hover) {
			return "solid #919191af"
		}
		if (selected) {
			return "solid #91919150"
		}

		return "solid rgba(0, 0, 0, 0)"
	}

	var playerProps = props.playerProps;

	const cardStyle: CSSProperties = {
		display: 'flex',
		flexDirection: 'column',
		borderRadius: '8px',
		border: determineBorder(),
		marginBottom: '10px',
		paddingBottom: '10px',
		backgroundColor: '#91919112',
		width: '440px',
	}
	const cardHeaderStyle: CSSProperties = {
		fontSize: '1.5em',
		fontWeight: 'bold',
		marginBottom: '5px',
		height: '40px',
		alignItems: 'center',
		justifyContent: 'center',
		display: 'flex',
		borderRadius: '8px 8px 0px 0px',
		backgroundColor: '#443f3fff',
		paddingLeft: '10px',
	}

	const headerButtonStyle: CSSProperties = {
		marginLeft: 'auto',
		marginRight: '10px'
	}

	const headerStyle: CSSProperties = {
		fontSize: '1em',
		fontWeight: 'bold',
		marginBottom: '5px',
		marginRight: '10px',
		overflow: 'hidden',
		textOverflow: 'ellipsis',
		whiteSpace: 'nowrap',
	}


	const handleDelete = async () => {
		if (props.deleteAction) {
			await props.deleteAction(playerProps.id);
		}
	}

	const handleGiveXp = async () => {
		if (props.giveXpAction) {
			await props.giveXpAction(playerProps, 100);
		}
	}

	const handleClick = (event: React.MouseEvent) => {
		setSelected(true)
		props.onClickAction?.(props.index, playerProps);
	}

	return (
		<div style={cardStyle} onMouseEnter={() => setHover(true)} onMouseLeave={() => setHover(false)} onClick={handleClick}>
			<div style={cardHeaderStyle}>
				<h2 style={headerStyle} title={playerProps.name}>{playerProps.name}</h2>
				<button onClick={handleGiveXp} style={headerButtonStyle}>Give XP</button>
				<button onClick={handleDelete} style={headerButtonStyle}>Delete</button>
			</div>
			<div>
				<p>Level: {playerProps.level}</p>
				<p>XP: {playerProps.xp}</p>
				<p>{playerProps.description}</p>
			</div>
		</div>
	);
}
export default PlayerCard;