interface Rank {
  rank: string;
  iconUrl: string;
}

interface Game {
  name: string;
  ranks: Rank[];
}
export interface Daily {
  quiz1: number;
  quiz2: number;
  quiz3: number;
  typeId: number;
  date: string;
  typeQuiz: any | null; // Nếu cần, thay thế `any` bằng kiểu cụ thể
}

export interface Quiz {
  quizId: number;
  region: string;
  url: string;
  correctValue: string;
  createdAt: string;
  userId: number;
  typeId: number;
  typeQuiz: any | null; // Nếu cần, thay thế `any` bằng kiểu cụ thể
  userAnswers: any[]; // Nếu cần, thay thế `any` bằng kiểu cụ thể
}

export interface DailyWithQuizzes extends Daily {
  quizzes: Quiz[];
}
