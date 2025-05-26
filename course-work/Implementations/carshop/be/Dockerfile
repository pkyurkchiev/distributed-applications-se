# --- Build stage ---
FROM maven:3.9.5-eclipse-temurin-21 AS build
WORKDIR /app
COPY . .
RUN mvn clean package -DskipTests

# --- Run stage ---
FROM openjdk:21-jdk-buster
WORKDIR /app
COPY --from=build /app/target/carshop-0.0.1-SNAPSHOT.jar app.jar
EXPOSE 8080
CMD ["java", "-jar", "app.jar"]
