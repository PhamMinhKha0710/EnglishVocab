import type { VocabularyWord } from "@/types"
// Import extended vocabulary
import { extendedVocabulary } from "./extended-vocabulary"

export const vocabularyCategories = [
  "Business",
  "Travel",
  "Academic",
  "Daily Life",
  "Technology",
  "Food & Cooking",
  "Health",
  "Entertainment",
  "Sports",
  "Nature",
]

// Combine original and extended vocabulary
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
    createdAt: new Date("2024-01-01"),
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
    createdAt: new Date("2024-01-02"),
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
    createdAt: new Date("2024-01-03"),
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
    createdAt: new Date("2024-01-04"),
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
    createdAt: new Date("2024-01-05"),
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
    createdAt: new Date("2024-01-06"),
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
    createdAt: new Date("2024-01-07"),
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
    createdAt: new Date("2024-01-08"),
  },
  {
    id: 9,
    word: "Championship",
    meaning: "Giải vô địch, chức vô địch",
    example: "Our team won the championship this year.",
    pronunciation: "/ˈtʃæmpiənʃɪp/",
    difficulty: "intermediate",
    category: "Sports",
    learned: false,
    reviewCount: 0,
    createdAt: new Date("2024-01-09"),
  },
  {
    id: 10,
    word: "Ecosystem",
    meaning: "Hệ sinh thái",
    example: "The forest ecosystem is very complex.",
    pronunciation: "/ˈiːkoʊˌsɪstəm/",
    difficulty: "advanced",
    category: "Nature",
    learned: false,
    reviewCount: 0,
    createdAt: new Date("2024-01-10"),
  },
  ...extendedVocabulary,
]

export const badges = [
  {
    id: "first-week",
    name: "Tuần đầu tiên",
    description: "Hoàn thành 7 ngày liên tiếp",
    icon: "🔥",
    category: "streak" as const,
  },
  {
    id: "beginner",
    name: "Người mới bắt đầu",
    description: "Học 25 từ vựng đầu tiên",
    icon: "🌟",
    category: "learning" as const,
  },
  {
    id: "dedicated",
    name: "Chăm chỉ",
    description: "Học 50 từ trong tuần",
    icon: "💪",
    category: "achievement" as const,
  },
  {
    id: "speed-learner",
    name: "Học nhanh",
    description: "Hoàn thành session dưới 3 phút",
    icon: "⚡",
    category: "achievement" as const,
  },
  {
    id: "perfectionist",
    name: "Hoàn hảo",
    description: "Đạt 100% trong session",
    icon: "🎯",
    category: "achievement" as const,
  },
]
