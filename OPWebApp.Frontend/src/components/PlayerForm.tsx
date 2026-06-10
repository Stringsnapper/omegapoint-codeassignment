import { useEffect, useState, type CSSProperties } from "react";
import type { PlayerProps } from "../interfaces/Player";

interface PlayerFormProps {
	submitAction: (playerProps:PlayerProps) => Promise<boolean>;
	playerProps?: PlayerProps;
}
var playerProps : PlayerProps = {
	id: '',
	name: '',
	level: 1,
	xp: 0,
	description: '',
}
const PlayerForm: React.FC<PlayerFormProps> = ({submitAction}) => {
	const [nameValue, setNameValue] = useState('');
	const [descriptionValue, setDescriptionValue] = useState('');
	const [submitDisabled, setSubmitDisabled] = useState(true);

	// Disable submit button if either name or description is empty
	useEffect(() => {
		setSubmitDisabled(nameValue.trim() === '' || descriptionValue.trim() === '') 
			
	}, [nameValue, descriptionValue]);


	const outerStyle : CSSProperties = {
		display: 'flex',
		flexDirection: 'column',
		textAlign: 'center',
		justifyContent: 'center',
		alignItems: 'center',
		padding: '20px',
		border: '1px solid #ccc',
		borderRadius: '8px',
		width: '400px',
	}
	const labelStyle : CSSProperties = {
		display: 'block',
		marginBottom: '10px',
		alignItems: 'center',
		justifyContent: 'center',
		justifySelf: 'center',
		textAlign: 'left',
		width: '100%',
		verticalAlign: 'middle',
				
	}

	const textInputStyle : CSSProperties = {
		width: '100%',
		height: '30px',
		border: '1px solid #aaaaaa38',
		backgroundColor: '#aaaaaa0a',
		borderRadius: '4px',
		fontFamily: 'monospace, monospace',
	}
	const textareaStyle : CSSProperties = {
		width: '100%',
		height: '70px',
		resize: 'none',
		border: '1px solid #aaaaaa38',
		backgroundColor: '#aaaaaa0a',
		borderRadius: '4px',
		fontFamily: 'monospace, monospace',
	}

	const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
		setNameValue(event.target.name === 'name' ? event.target.value : nameValue);
		setDescriptionValue(event.target.name === 'description' ? event.target.value : descriptionValue);
	};

	const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
		event.preventDefault();
		playerProps.name = nameValue;
		playerProps.description = descriptionValue;
		var success = await submitAction(playerProps);
		console.debug("Submit action returned:", success);
		if (success) {
			setNameValue('');
			setDescriptionValue('');
		}
	};


	return (
		<div style={outerStyle}>
			<form onSubmit={handleSubmit} id="main-form" >
				<label style={labelStyle}>
					Player Name:
					<input value={nameValue} onChange={handleInputChange} style={textInputStyle} type="text" name="name"/>
				</label>
				<label style={labelStyle}>
					Description:
					<textarea value={descriptionValue} onChange={handleInputChange} style={textareaStyle} name="description" />
				</label>
				<button disabled={submitDisabled} type="submit">Create Player</button>
			</form>
		</div>
	)

}

export default PlayerForm;