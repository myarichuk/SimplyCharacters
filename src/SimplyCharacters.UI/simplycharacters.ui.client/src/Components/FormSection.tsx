import styled from "styled-components";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;
const Label = styled.label`
  font-weight: bold;
`;

const Description = styled.p`
  font-weight: normal;
  color: lightgrey;
`;

interface FormSectionProps {
  label: string;
  description?: string;
  children: React.ReactNode; // Accept ReactNode as children
}
export const FormSection: React.FC<FormSectionProps> = ({
  label,
  description,
  children,
}) => {
  return (
    <Wrapper>
      <Label>{label}</Label>
      <Description>{description}</Description>
      {children}
    </Wrapper>
  );
};
