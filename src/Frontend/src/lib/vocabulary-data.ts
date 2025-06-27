export interface VocabularyWord {
  id: number
  word: string
  meaning: string
  example: string
  pronunciation: string
  difficulty: "beginner" | "intermediate" | "advanced"
  category: string
  learned: boolean
  reviewCount: number
  lastReviewed?: Date
  nextReview?: Date
}

export const vocabularyCategories = [
  "Business",
  "Travel",
  "Academic",
  "Daily Life",
  "Technology",
  "Food & Cooking",
  "Health",
  "Entertainment",
]

export const vocabularyData: VocabularyWord[] = [
  {
    id: 1,
    word: "Serendipity",
    meaning: "Một bất ngờ thú vị hoặc tai nạn may mắn",
    example: "Finding this book was pure serendipity.",
    pronunciation: "/ˌserənˈdipədē/",
    difficulty: "advanced",
    category: "Academic",
    learned: false,
    reviewCount: 0,
  },
  {
    id: 2,
    word: "Efficient",
    meaning: "Hiệu quả, làm việc tốt mà không lãng phí thời gian",
    example: "She's very efficient at managing her time.",
    pronunciation: "/ɪˈfɪʃənt/",
    difficulty: "intermediate",
    category: "Business",
    learned: false,
    reviewCount: 0,
  },
  {
    id: 3,
    word: "Reservation",
    meaning: "Đặt chỗ trước",
    example: "I made a reservation at the restaurant.",
    pronunciation: "/ˌrezərˈveɪʃən/",
    difficulty: "beginner",
    category: "Travel",
    learned: false,
    reviewCount: 0,
  },
  {
    id: 4,
    word: "Algorithm",
    meaning: "Thuật toán, quy trình giải quyết vấn đề",
    example: "The algorithm processes data efficiently.",
    pronunciation: "/ˈælɡəˌrɪðəm/",
    difficulty: "advanced",
    category: "Technology",
    learned: false,
    reviewCount: 0,
  },
  {
    id: 5,
    word: "Delicious",
    meaning: "Ngon, có vị ngon",
    example: "This cake is absolutely delicious!",
    pronunciation: "/dɪˈlɪʃəs/",
    difficulty: "beginner",
    category: "Food & Cooking",
    learned: false,
    reviewCount: 0,
  },
  {
    id: 6,
    word: "Collaborate",
    meaning: "Hợp tác, làm việc cùng nhau",
    example: "We need to collaborate on this project.",
    pronunciation: "/kəˈlæbəˌreɪt/",
    difficulty: "intermediate",
    category: "Business",
    learned: false,
    reviewCount: 0,
  },
  {
    id: 7,
    word: "Symptoms",
    meaning: "Triệu chứng bệnh",
    example: "What are the symptoms of this illness?",
    pronunciation: "/ˈsɪmptəmz/",
    difficulty: "intermediate",
    category: "Health",
    learned: false,
    reviewCount: 0,
  },
  {
    id: 8,
    word: "Entertainment",
    meaning: "Giải trí, sự giải trí",
    example: "Music is my favorite form of entertainment.",
    pronunciation: "/ˌentərˈteɪnmənt/",
    difficulty: "intermediate",
    category: "Entertainment",
    learned: false,
    reviewCount: 0,
  },
]
