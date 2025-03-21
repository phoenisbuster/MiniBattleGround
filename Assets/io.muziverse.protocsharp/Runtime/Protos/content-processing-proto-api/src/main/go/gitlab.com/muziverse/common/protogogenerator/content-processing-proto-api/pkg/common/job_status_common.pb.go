// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: common/job_status_common.proto

package common

import (
	protoreflect "google.golang.org/protobuf/reflect/protoreflect"
	protoimpl "google.golang.org/protobuf/runtime/protoimpl"
	reflect "reflect"
	sync "sync"
)

const (
	// Verify that this generated code is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(20 - protoimpl.MinVersion)
	// Verify that runtime/protoimpl is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(protoimpl.MaxVersion - 20)
)

type JobStatus int32

const (
	JobStatus_JOB_STATUS_UNSPECIFIED JobStatus = 0
	JobStatus_NEW                    JobStatus = 1
	JobStatus_IN_PROGRESS            JobStatus = 2
	JobStatus_STOPPED                JobStatus = 3
	JobStatus_FAILED                 JobStatus = 4
	JobStatus_SUCCESSED              JobStatus = 5
)

// Enum value maps for JobStatus.
var (
	JobStatus_name = map[int32]string{
		0: "JOB_STATUS_UNSPECIFIED",
		1: "NEW",
		2: "IN_PROGRESS",
		3: "STOPPED",
		4: "FAILED",
		5: "SUCCESSED",
	}
	JobStatus_value = map[string]int32{
		"JOB_STATUS_UNSPECIFIED": 0,
		"NEW":                    1,
		"IN_PROGRESS":            2,
		"STOPPED":                3,
		"FAILED":                 4,
		"SUCCESSED":              5,
	}
)

func (x JobStatus) Enum() *JobStatus {
	p := new(JobStatus)
	*p = x
	return p
}

func (x JobStatus) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (JobStatus) Descriptor() protoreflect.EnumDescriptor {
	return file_common_job_status_common_proto_enumTypes[0].Descriptor()
}

func (JobStatus) Type() protoreflect.EnumType {
	return &file_common_job_status_common_proto_enumTypes[0]
}

func (x JobStatus) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use JobStatus.Descriptor instead.
func (JobStatus) EnumDescriptor() ([]byte, []int) {
	return file_common_job_status_common_proto_rawDescGZIP(), []int{0}
}

var File_common_job_status_common_proto protoreflect.FileDescriptor

var file_common_job_status_common_proto_rawDesc = []byte{
	0x0a, 0x1e, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x6a, 0x6f, 0x62, 0x5f, 0x73, 0x74, 0x61,
	0x74, 0x75, 0x73, 0x5f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f,
	0x2a, 0x69, 0x0a, 0x09, 0x4a, 0x6f, 0x62, 0x53, 0x74, 0x61, 0x74, 0x75, 0x73, 0x12, 0x1a, 0x0a,
	0x16, 0x4a, 0x4f, 0x42, 0x5f, 0x53, 0x54, 0x41, 0x54, 0x55, 0x53, 0x5f, 0x55, 0x4e, 0x53, 0x50,
	0x45, 0x43, 0x49, 0x46, 0x49, 0x45, 0x44, 0x10, 0x00, 0x12, 0x07, 0x0a, 0x03, 0x4e, 0x45, 0x57,
	0x10, 0x01, 0x12, 0x0f, 0x0a, 0x0b, 0x49, 0x4e, 0x5f, 0x50, 0x52, 0x4f, 0x47, 0x52, 0x45, 0x53,
	0x53, 0x10, 0x02, 0x12, 0x0b, 0x0a, 0x07, 0x53, 0x54, 0x4f, 0x50, 0x50, 0x45, 0x44, 0x10, 0x03,
	0x12, 0x0a, 0x0a, 0x06, 0x46, 0x41, 0x49, 0x4c, 0x45, 0x44, 0x10, 0x04, 0x12, 0x0d, 0x0a, 0x09,
	0x53, 0x55, 0x43, 0x43, 0x45, 0x53, 0x53, 0x45, 0x44, 0x10, 0x05, 0x42, 0x95, 0x01, 0x0a, 0x2b,
	0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x2e, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74, 0x70, 0x72, 0x6f, 0x63, 0x65, 0x73,
	0x73, 0x69, 0x6e, 0x67, 0x2e, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x42, 0x0e, 0x4a, 0x6f, 0x62,
	0x53, 0x74, 0x61, 0x74, 0x75, 0x73, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x50, 0x01, 0x5a, 0x54, 0x67,
	0x69, 0x74, 0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65,
	0x72, 0x73, 0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x70, 0x72, 0x6f, 0x74, 0x6f,
	0x67, 0x6f, 0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x2f, 0x63, 0x6f, 0x6e, 0x74,
	0x65, 0x6e, 0x74, 0x2d, 0x70, 0x72, 0x6f, 0x63, 0x65, 0x73, 0x73, 0x69, 0x6e, 0x67, 0x2d, 0x70,
	0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69, 0x2f, 0x70, 0x6b, 0x67, 0x2f, 0x63, 0x6f, 0x6d,
	0x6d, 0x6f, 0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_common_job_status_common_proto_rawDescOnce sync.Once
	file_common_job_status_common_proto_rawDescData = file_common_job_status_common_proto_rawDesc
)

func file_common_job_status_common_proto_rawDescGZIP() []byte {
	file_common_job_status_common_proto_rawDescOnce.Do(func() {
		file_common_job_status_common_proto_rawDescData = protoimpl.X.CompressGZIP(file_common_job_status_common_proto_rawDescData)
	})
	return file_common_job_status_common_proto_rawDescData
}

var file_common_job_status_common_proto_enumTypes = make([]protoimpl.EnumInfo, 1)
var file_common_job_status_common_proto_goTypes = []interface{}{
	(JobStatus)(0), // 0: JobStatus
}
var file_common_job_status_common_proto_depIdxs = []int32{
	0, // [0:0] is the sub-list for method output_type
	0, // [0:0] is the sub-list for method input_type
	0, // [0:0] is the sub-list for extension type_name
	0, // [0:0] is the sub-list for extension extendee
	0, // [0:0] is the sub-list for field type_name
}

func init() { file_common_job_status_common_proto_init() }
func file_common_job_status_common_proto_init() {
	if File_common_job_status_common_proto != nil {
		return
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_common_job_status_common_proto_rawDesc,
			NumEnums:      1,
			NumMessages:   0,
			NumExtensions: 0,
			NumServices:   0,
		},
		GoTypes:           file_common_job_status_common_proto_goTypes,
		DependencyIndexes: file_common_job_status_common_proto_depIdxs,
		EnumInfos:         file_common_job_status_common_proto_enumTypes,
	}.Build()
	File_common_job_status_common_proto = out.File
	file_common_job_status_common_proto_rawDesc = nil
	file_common_job_status_common_proto_goTypes = nil
	file_common_job_status_common_proto_depIdxs = nil
}
