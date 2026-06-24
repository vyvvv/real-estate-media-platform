export type Property ={

    id:number;
    title:string;
    description:string;
    street:string;
    city:string;
    state:string;
    postcode:number;
    longitude: number| null;    
    latitude: number | null;
    price:number;
    bedrooms:number;
    bathrooms:number;
    garages:number;
    floorArea:number;
    createdAt:Date;
    isDeleted:boolean;
    propertyType:number;
    saleCategory:number;
    listingCaseStatus:number;
    userId:string | null;

}


export interface PropertyFormProps {
  initialData?: Property | null;
}