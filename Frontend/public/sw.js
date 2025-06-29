const CACHE_NAME = "flashlearn-v1"
const urlsToCache = ["/", "/static/js/bundle.js", "/static/css/main.css", "/manifest.json"]

// Install event
self.addEventListener("install", (event) => {
  event.waitUntil(
    caches.open(CACHE_NAME).then((cache) => {
      return cache.addAll(urlsToCache)
    }),
  )
})

// Fetch event
self.addEventListener("fetch", (event) => {
  event.respondWith(
    caches.match(event.request).then((response) => {
      // Return cached version or fetch from network
      return response || fetch(event.request)
    }),
  )
})

// Background sync for offline data
self.addEventListener("sync", (event) => {
  if (event.tag === "background-sync") {
    event.waitUntil(syncOfflineData())
  }
})

async function syncOfflineData() {
  // Sync offline data when connection is restored
  const pendingData = await getStoredData()
  if (pendingData) {
    try {
      await fetch("/api/sync", {
        method: "POST",
        body: JSON.stringify(pendingData),
        headers: {
          "Content-Type": "application/json",
        },
      })
      // Clear stored data after successful sync
      await clearStoredData()
    } catch (error) {
      console.error("Background sync failed:", error)
    }
  }
}

// Declare getStoredData and clearStoredData functions
async function getStoredData() {
  // Implement logic to retrieve stored data
  // This is a placeholder implementation
  return JSON.parse(localStorage.getItem("pendingData"))
}

async function clearStoredData() {
  // Implement logic to clear stored data
  // This is a placeholder implementation
  localStorage.removeItem("pendingData")
}
