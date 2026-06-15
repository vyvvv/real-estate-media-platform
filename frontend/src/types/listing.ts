export enum PropertyType {
  House = 0,
  Unit = 1,
  Townhouse = 2,
  Villa = 3,
  Others = 4,
}

export enum SaleCategory {
  ForSale = 0,
  ForRent = 1,
  Auction = 2,
}

export enum ListingCaseStatus {
  Created = 0,
  Pending = 1,
  Delivered = 2,
}

export enum MediaType {
  Photo = 0,
  Video = 1,
  FloorPlan = 2,
  VrTour = 3,
}

export interface ListingCase {
  id: number;

  title: string;
  description: string;

  street: string;
  city: string;
  state: string;
  postcode: number;

  longitude: number;
  latitude: number;

  price: number;
  bedrooms: number;
  bathrooms: number;
  garages: number;
  floorArea: number;

  createdAt: string;
  isDeleted: boolean;

  propertyType: PropertyType;
  saleCategory: SaleCategory;
  listingCaseStatus: ListingCaseStatus;

  userId: string;

  mediaAssets?: MediaAsset[];
  caseContacts?: CaseContact[];
  assignedAgents?: Agent[];
}

export interface MediaAsset {
  id: number;

  mediaType: MediaType;
  mediaUrl: string;

  uploadedAt: string;

  isSelect: boolean;
  isHero: boolean;
  isDeleted: boolean;

  listingCaseId: number;
  userId: string;
}

export interface CaseContact {
  contactId: number;

  firstName: string;
  lastName: string;
  companyName: string;

  profileUrl: string;
  email: string;
  phoneNumber: string;

  listingCaseId: number;
}

export interface Agent {
  id: string;

  agentFirstName: string;
  agentLastName: string;

  avatarUrl: string;
  companyName: string;
}

export interface AgentListingCase {
  agentId: string;
  listingCaseId: number;
}


export function getPropertyTypeLabel(type: PropertyType): string {
  switch (type) {
    case PropertyType.House:
      return "House";
    case PropertyType.Unit:
      return "Unit";
    case PropertyType.Townhouse:
      return "Townhouse";
    case PropertyType.Villa:
      return "Villa";
    case PropertyType.Others:
      return "Others";
    default:
      return "Unknown";
  }
}

export function getSaleCategoryLabel(category: SaleCategory): string {
  switch (category) {
    case SaleCategory.ForSale:
      return "For Sale";
    case SaleCategory.ForRent:
      return "For Rent";
    case SaleCategory.Auction:
      return "Auction";
    default:
      return "Unknown";
  }
}

export function getListingCaseStatusLabel(status: ListingCaseStatus): string {
  switch (status) {
    case ListingCaseStatus.Created:
      return "Created";
    case ListingCaseStatus.Pending:
      return "Pending";
    case ListingCaseStatus.Delivered:
      return "Delivered";
    default:
      return "Unknown";
  }
}

export function getMediaTypeLabel(type: MediaType): string {
  switch (type) {
    case MediaType.Photo:
      return "Photo";
    case MediaType.Video:
      return "Video";
    case MediaType.FloorPlan:
      return "Floor Plan";
    case MediaType.VrTour:
      return "VR Tour";
    default:
      return "Unknown";
  }
}