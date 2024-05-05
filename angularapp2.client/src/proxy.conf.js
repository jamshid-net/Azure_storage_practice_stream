const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://localhost:51032",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
