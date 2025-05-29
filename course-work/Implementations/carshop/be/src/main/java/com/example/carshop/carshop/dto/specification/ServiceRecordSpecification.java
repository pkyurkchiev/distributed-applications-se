package com.example.carshop.carshop.dto.specification;

import com.example.carshop.carshop.entities.impl.ServiceCarEntity;
import com.example.carshop.carshop.entities.impl.ServiceRecordEntity;
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

public class ServiceRecordSpecification implements Specification<ServiceRecordEntity> {

    private final Map<String, String> filters;

    public ServiceRecordSpecification(Map<String, String> filters) {
        this.filters = filters;
    }

    @Override
    public Predicate toPredicate(Root<ServiceRecordEntity> root, CriteriaQuery<?> query, CriteriaBuilder cb) {
        List<Predicate> predicates = new ArrayList<>();

        filters.forEach((key, value) -> {
            if (value == null || value.isEmpty()) {
                return;
            }
            switch (key) {
                case "customer_id":
                    predicates.add(cb.equal(root.get("serviceCarId").get("customer").get("id"), value));
                    break;
                case "carMake":
                    predicates.add(cb.equal(root.get("carMake"), value));
                    break;
                case "carModel":
                    predicates.add(cb.equal(root.get("carModel"), value));
                    break;
                case "licensePlate":
                    predicates.add(cb.equal(root.get("licensePlate"), value));
                    break;
                case "employeeFirstName":
                    predicates.add(cb.equal(root.get("employeeFirstName"), value));
                    break;
                case "employeeLastName":
                    predicates.add(cb.equal(root.get("employeeLastName"), value));
                    break;
                case "serviceDate":
                    predicates.add(cb.equal(root.get("serviceDate"), LocalDate.parse(value)));
                    break;
                case "costFrom":
                    predicates.add(cb.greaterThanOrEqualTo(root.get("serviceCost"), new BigDecimal(value)));
                    break;
                case "costTo":
                    predicates.add(cb.lessThanOrEqualTo(root.get("serviceCost"), new BigDecimal(value)));
                    break;
            }
        });

        return cb.and(predicates.toArray(new Predicate[0]));
    }
}
