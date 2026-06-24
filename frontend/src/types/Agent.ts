export type Agent = {
    contactId:number;
    firstName: string;
    lastName:string;
    companyName:string;
    profileUrl:string;
    email:string;
    phoneNumber:string;
    listingCaseId:number;

}

export interface AgentFormProps {
  initialData?: Agent | null;
}