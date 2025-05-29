package com.example.carshop.carshop.dto.specification;

import com.example.carshop.carshop.entities.impl.CarEntity;
import com.example.carshop.carshop.entities.impl.ServiceCarEntity;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Predicate;
import jakarta.persistence.criteria.Root;
import org.springframework.data.jpa.domain.Specification;


import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class ServiceCarSpecification implements Specification<ServiceCarEntity> {

    private final Map<String, String> filters;

    public ServiceCarSpecification(Map<String, String> filters) {
        this.filters = filters;
    }

    @Override
    public Predicate toPredicate(Root<ServiceCarEntity> root, CriteriaQuery<?> query, CriteriaBuilder cb) {
        List<Predicate> predicates = new ArrayList<>();

        filters.forEach((key, value) -> {
            if (value == null || value.isEmpty()) {
                return;
            }
            switch (key) {
                case "customer_id":
                    predicates.add(cb.equal(root.get("customer").get("id"), value));
                    break;
                case "make":
                    predicates.add(cb.equal(root.get("make"), value));
                    break;
                case "model":
                    predicates.add(cb.equal(root.get("model"), value));
                    break;
                case "manufactureYear":
                    predicates.add(cb.equal(root.get("manufactureYear"), LocalDate.parse(value)));
                    break;
                case "licensePlate":
                    predicates.add(cb.equal(root.get("licensePlate"), value));
                    break;
            }
        });

        return cb.and(predicates.toArray(new Predicate[0]));
    }
}
