package com.example.carshop.authentication.config;

import lombok.NonNull;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class CorsConfiguration {

    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {
            @Override
            public void addCorsMappings(@NonNull CorsRegistry registry) {
                registry.addMapping("/**")
                        .allowedOrigins("http://localhost:80", "http://localhost", "http://localhost:3000")
                        .allowedMethods("*")
                        .allowedHeaders("*")
                        .allowCredentials(true);
            }
        };
    }
}
