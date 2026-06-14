import { useEffect, useState, type CSSProperties } from "react";
import type { PlayerProps } from "../interfaces/Player";

interface PlayerFormProps {
	submitAction: (playerProps:PlayerProps) => Promise<boolean>;
	updateAction: (playerProps:PlayerProps) => Promise<boolean>;
	playerProps?: PlayerProps;
}

const PlayerForm: React.FC<PlayerFormProps> = (props) => {
	const [nameValue, setNameValue] = useState('');
	const [descriptionValue, setDescriptionValue] = useState('');
	const [submitDisabled, setSubmitDisabled] = useState(true);
	const [updateDisabled, setUpdateDisabled] = useState(true);

	// Disable submit button if either name or description is empty
	useEffect(() => {
		setSubmitDisabled(props.playerProps!==null || nameValue.trim() === '' || descriptionValue.trim() === '') 
		setUpdateDisabled(props.playerProps===null || nameValue.trim() === '' || descriptionValue.trim() === '')
	}, [nameValue, descriptionValue, props.playerProps]);

	useEffect(() => {
		if (props.playerProps != null) {
			console.debug("Updating form with playerProps:", props.playerProps);
			setNameValue(props.playerProps.name);
			setDescriptionValue(props.playerProps.description);
		}
	}, [props.playerProps])


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
	const buttonStyle: CSSProperties = {
		marginLeft: '5px',
		marginRight: '5px',
	}

	const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
		setNameValue(event.target.name === 'name' ? event.target.value : nameValue);
		setDescriptionValue(event.target.name === 'description' ? event.target.value : descriptionValue);
	};

	const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
		event.preventDefault();
		var success = await props.submitAction({name: nameValue, description: descriptionValue} as PlayerProps);
		console.debug("Submit action returned:", success);
		if (success) {
			setNameValue('');
			setDescriptionValue('');
		}
	};

	const handleUpdatePlayer = async () => {
		props.playerProps.name = nameValue;
		props.playerProps.description = descriptionValue;
		var success = await props.updateAction(props.playerProps);
		console.debug("Update action returned:", success);
		if (success) {
			setNameValue('');
			setDescriptionValue('');
		}
	}


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
				<button style={buttonStyle} disabled={updateDisabled} type="button" onClick={() => {handleUpdatePlayer()}}>Update Player</button>
				<button style={buttonStyle} disabled={submitDisabled} type="submit">Create Player</button>
			</form>
		</div>
	)

}

export default PlayerForm;