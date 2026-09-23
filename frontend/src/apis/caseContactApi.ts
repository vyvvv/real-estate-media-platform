// 前端的Agent.ts文件和后端的CaseContact是一个东西

import { type Agent } from "../types/Agent";

const API_BASE_URL = "http://localhost:5166";

export async function getCaseContacts():Promise<Agent[]>{

    const response = await fetch(`${API_BASE_URL}/api/CaseContact`)
    if (!response.ok) {
        throw new Error("Failed to fetch case contacts");
    }
    const caseContacts = await response.json();
    return caseContacts;
}

