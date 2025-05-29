package com.example.carshop.carshop.dto.specification;


import com.example.carshop.carshop.entities.impl.CarEntity;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Predicate;
import jakarta.persistence.criteria.Root;
import org.springframework.data.jpa.domain.Specification;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;


public class CarSpecification implements Specification<CarEntity> {

    private final Map<String, String> filters;

    public CarSpecification(Map<String, String> filters) {
        this.filters = filters;
    }

    @Override
    public Predicate toPredicate(Root<CarEntity> root, CriteriaQuery<?> query, CriteriaBuilder cb) {
        List<Predicate> predicates = new ArrayList<>();

        filters.forEach((key, value) -> {
            if (value == null || value.isEmpty()) {
                return;
            }
            switch (key) {
                case "make":
                    predicates.add(cb.equal(root.get("make"), value));
                    break;
                case "model":
                    predicates.add(cb.equal(root.get("model"), value));
                    break;
                case "manufactureYear":
                    predicates.add(cb.equal(root.get("manufactureYear"), LocalDate.parse(value)));
                    break;
                case "priceFrom":
                    predicates.add(cb.ge(root.get("price"), new BigDecimal(value)));
                    break;
                case "priceTo":
                    predicates.add(cb.le(root.get("price"), new BigDecimal(value)));
                    break;
                case "isInStock":
                    predicates.add(cb.equal(root.get("isInStock"), Boolean.parseBoolean(value)));
                    break;
            }
        });

        return cb.and(predicates.toArray(new Predicate[0]));
    }
}
