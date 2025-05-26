# Define build argument (default is dev)
ARG ENV=dev

FROM node:18-alpine AS prod
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Production Stage with Nginx
FROM nginx:1.24-alpine AS production
COPY --from=prod /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
# Serve production build with Nginx
