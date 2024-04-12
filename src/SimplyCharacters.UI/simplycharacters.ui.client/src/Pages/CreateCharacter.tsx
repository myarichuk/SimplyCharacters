import { useState } from "react";
import {
  abilitiesGenerationOptions,
  genders,
  levels,
} from "../globalVariables";
import { FormSection } from "../Components/FormSection";
import { InputLabel, MenuItem, Select, TextField } from "@mui/material";

interface iCreateCharacterProps {}

const CreateCharacter: React.FC<iCreateCharacterProps> = () => {
  const [character, setCharacter] = useState<Character>({
    CharacterName: "",
    CharacterLevel: "",
    Gender: "",
    abilitiesGeneration: "",
  });

  return (
    <div>
      <h1>New Character</h1>
      <h3>Character Details:</h3>
      <FormSection label={"Name"}>
        <TextField
          id="CharacterName"
          label="Name"
          variant="outlined"
          onChange={(e) => {
            setCharacter((prevState) => ({
              ...prevState,
              CharacterName: e.target.value,
            }));
          }}
        />
      </FormSection>
      <FormSection label="Starting level">
        <InputLabel id="level-label">Level</InputLabel>
        <Select
          labelId="level-label"
          id="level"
          value={character.CharacterLevel}
          label="Level"
          onChange={(e) => {
            setCharacter((prevState) => ({
              ...prevState,
              CharacterLevel: e.target.value,
            }));
          }}
        >
          {levels.map((lvl) => (
            <MenuItem value={lvl}>{lvl}</MenuItem>
          ))}
        </Select>
      </FormSection>

      <FormSection label="Gender">
        <InputLabel id="Gender-label">Gender</InputLabel>
        <Select
          labelId="Gender-label"
          id="Gender"
          value={character.Gender}
          label="Gender"
          onChange={(e) => {
            setCharacter((prevState) => ({
              ...prevState,
              Gender: e.target.value,
            }));
          }}
        >
          {genders.map((gender) => (
            <MenuItem value={gender}>{gender}</MenuItem>
          ))}
        </Select>
      </FormSection>
      <FormSection label="">
        <InputLabel id="Abilities-label">
          Abilities Generation Option
        </InputLabel>
        <Select
          labelId="AbilitiesGeneration-label"
          id="AbilitiesGeneration"
          value={character.abilitiesGeneration}
          label=" Abilities Generation Option"
          onChange={(e) => {
            setCharacter((prevState) => ({
              ...prevState,
              abilitiesGeneration: e.target.value,
            }));
          }}
        >
          {abilitiesGenerationOptions.map((option) => (
            <MenuItem value={option.type}>{option.text}</MenuItem>
          ))}
        </Select>
      </FormSection>

      <br />
      <br />
      <br />
      <br />
      <div>
        <div>{character.CharacterName}</div>
        <div>{character.CharacterLevel}</div>
        <div>{character.Gender}</div>
        <div>{character.abilitiesGeneration}</div>
      </div>
    </div>
  );
};

export default CreateCharacter;
